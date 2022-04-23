import React, { Component } from 'react';
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip } from 'recharts';
import { getData } from '../util';
import './MessageList.css'

export class DashboardLastMessages extends Component {

    constructor(props) {
        super(props);
        this.state = { loading: false, messages: [] }
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const response = await getData("api/dashboard/lastMessages");
        this.setState({ loading: false, messages: response });
    }

    render() {
        const data = this.state.messages.map(measurement => {
            var date = new Date(measurement.dateTime);
            return {
                "name": date.toLocaleString(), "Негативные обращения": measurement.threats
            }
        })
        return (
            <div class="container border border-dark rounded">
                <div class="row d-flex justify-content-center mb-2">
                    <h5 class="mt-2 mb-2">Последние обращения</h5>
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading && this.state.messages.map(message => 
                        <ul class="list-group">
                            <li class="list-group-item d-flex justify-content-between align-content-center">
                                <div class="d-flex flex-row">
                                    <i class={`fa ${message.messageType == 1 ? "fa-exclamation-triangle" : "fa-question-circle"}`}></i>
                                    <div class="ml-2">
                                        <p class="mb-0">{ message.content }</p>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    )
                    }
                </div>
            </div>
        );
    }
}
