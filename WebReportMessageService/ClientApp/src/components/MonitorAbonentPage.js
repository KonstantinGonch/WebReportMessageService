import React, { Component } from 'react';
import { getData } from '../util';
import { MonitorAbonentPageResourceUsageChartWidget } from './MonitorAbonentPageResourceUsageChartWidget';
import { MonitorAbonentPageThreatChartWidget } from './MonitorAbonentPageThreatChartWidget';
import { MonitorAbonentPageThreatsWidget } from './MonitorAbonentPageThreatsWidget';
import './MessageList.css'

export class MonitorAbonentPage extends Component {

    constructor(props) {
        super(props);

        this.state = { loading: false, lastThreats: [], lastMeasurements: [] }
    }

    async componentDidMount() {
        let id = this.props.match.params.id;
        this.setState({ loading: true })

        const responseThreats = await getData("api/threat/monitorAbonentThreats?id=" + id);
        const responseMeasurements = await getData("api/monitorAbonent/actualMeasurements?abonentId=" + id);

        this.setState({ loading: false, lastThreats: responseThreats, lastMeasurements: responseMeasurements });

    }

    render() {
        return (
            <div class="container pt-3">
                <div class="row d-flex justify-content-center">
                    <h1>Информация о пользователе</h1>
                </div>
                {this.state.loading &&
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-primary list-spinner" role="status">
                            <span class="sr-only">Загрузка...</span>
                        </div>
                    </div>}
                {!this.state.loading &&
                    <>
                        <div class="row">
                            <div class="col-md-6">
                                <MonitorAbonentPageThreatChartWidget measurements={this.state.lastMeasurements} />
                            </div>
                            <div class="col-md-6">
                                <MonitorAbonentPageResourceUsageChartWidget measurements={this.state.lastMeasurements} />
                            </div>
                        </div>
                        <div class="row d-flex justify-content-center pb-3">
                            <h2>События безопасности</h2>
                            <div class="abonent-events-container overflow-auto">
                                <MonitorAbonentPageThreatsWidget threats={this.state.lastThreats} />
                            </div>
                        </div>
                    </>
                }
            </div>
        );
    }
}
