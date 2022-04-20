import React, { Component } from 'react';
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip } from 'recharts';
import './MessageList.css';

export class MonitorAbonentPageResourceUsageChartWidget extends Component {

    constructor(props) {
        super(props);
    }

    render() {

        const data = this.props.measurements.map(measurement => {
            var date = new Date(measurement.date);
            return {
                "name": date.toLocaleString(), "ЦП": measurement.cpuUsage, "RAM": measurement.ramUsage }
        })
        return (
            <div class="container monitor-user-widget">
                <div class="row d-flex justify-content-center">
                    <h3>Нагрузка на ресурсы</h3>
                    <div id="chart-place">
                        <AreaChart
                            width={500}
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
                            <Area type="monotone" dataKey="ЦП" stackId="1" stroke="#8884d8" fill="#8884d8" />
                            <Area type="monotone" dataKey="RAM" stackId="1" stroke="#82ca9d" fill="#82ca9d" />
                        </AreaChart>
                    </div>
                </div>
            </div>
        );
    }
}
