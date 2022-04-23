import React, { Component } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer, Tooltip } from 'recharts';
import { getData } from '../util';
import './MessageList.css'

export class DashboardScanRate extends Component {

    constructor(props) {
        super(props);
        this.state = { loading: false, scanRate: [] }
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const response = await getData("api/dashboard/scanRate");
        this.setState({ loading: false, scanRate: response });
    }

    render() {

        const data = [
            { name: 'Успешные сканирования', value: this.state.scanRate.goodScans, color: "#00C49F" },
            { name: 'Проблемные сканирования', value: this.state.scanRate.badScans, color: "#D50F0F" },
        ];
        return (
            <div class="container border border-dark rounded">
                <div class="row d-flex justify-content-center">
                    <h5 class="mt-2 mb-2">Рейтинг сканирований</h5>
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading &&
                        <div id="chart-place" class="d-flex justify-content-center">
                            <PieChart width={400} height={400}>
                                <Pie
                                    dataKey="value"
                                    data={data}
                                    cy="50%"
                                    innerRadius={100}
                                    outerRadius={150}
                                    fill="#8884d8"
                                    label
                                >
                                    {data.map(d => (
                                        <Cell key={d.name} fill={d.color} />
                                    ))}
                                </Pie>
                                <Tooltip />
                            </PieChart>
                        </div>
                    }
                </div>
            </div>
        );
    }
}
