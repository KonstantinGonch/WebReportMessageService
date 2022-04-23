import React, { Component } from 'react';
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip } from 'recharts';
import { getData } from '../util';
import './MessageList.css'

export class DashboardRegisteredThreats extends Component {

    constructor(props) {
        super(props);
        this.state = {loading: false, threatData: []}
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const response = await getData("api/dashboard/threats");
        this.setState({ loading: false, threatData: response });
    }

    render() {

        const data = this.state.threatData.map(measurement => {
            var date = new Date(measurement.dateTime);
            return {
                "name": date.toLocaleString(), "Угрозы": measurement.threats
            }
        })
        return (
            <div class="container border border-dark rounded">
                <div class="row d-flex justify-content-center">
                    <h5 class="mt-2 mb-2">Регистрируемые в системе угрозы</h5>
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading &&
                        <div id="chart-place">
                            <AreaChart
                                width={1200}
                                height={400}
                                data={data}
                                margin={{
                                    top: 10,
                                    right: 30,
                                    left: 0,
                                    bottom: 0,
                                }}
                            >
                                <CartesianGrid strokeDasharray="3 3" />
                                <XAxis dataKey="name" />
                                <YAxis />
                                <Tooltip />
                            <Area type="monotone" dataKey="Угрозы" stackId="1" stroke="#C83A3A" fill="#C83A3A" />
                            </AreaChart>
                        </div>
                    }
                </div>
            </div>
        );
    }
}
