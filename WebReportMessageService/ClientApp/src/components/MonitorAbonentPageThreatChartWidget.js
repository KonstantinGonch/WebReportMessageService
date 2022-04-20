import React, { Component } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer, Tooltip } from 'recharts';
import './MessageList.css';

export class MonitorAbonentPageThreatChartWidget extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        const goodMeasurements = this.props.measurements.filter(m => m.threatId > 0).length;
        const badMeasurements = this.props.measurements.filter(m => m.threatId === 0).length;
        const data = [
            { name: 'Плохие метрики', value: goodMeasurements, color: "#D50F0F" },
            { name: 'Хорошие метрики', value: badMeasurements, color: "#00C49F" },
        ];
        return (
            <div class="container monitor-user-widget">
                <div class="row d-flex justify-content-center">
                    <h3>Состояние безопасности</h3>
                    <div id="chart-place">
                        <PieChart width={400} height={400}>
                            <Pie
                                dataKey="value"
                                startAngle={180}
                                endAngle={0}
                                data={data}
                                cx="50%"
                                cy="65%"
                                outerRadius={150}
                                fill="#8884d8"
                                label
                            >
                                {data.map(d => (
                                    <Cell key={d.name} fill={d.color} />
                                ))}
                            </Pie>
                            <Tooltip/>
                        </PieChart>
                    </div>
                </div>
            </div>
        );
    }
}
