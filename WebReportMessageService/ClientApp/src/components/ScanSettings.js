import React, { Component } from 'react';
import { getData, postData } from '../util';

export class ScanSettings extends Component {

    constructor(props) {
        super(props);
        this.state = { loading: false, jobRestartMinutes: 60, pingRetries: 3, pingFailureThreat: 1 }

        this.onRestartChange = this.onRestartChange.bind(this);
        this.onRetriesChange = this.onRetriesChange.bind(this);
        this.onFailureChange = this.onFailureChange.bind(this);
        this.onSaveClick = this.onSaveClick.bind(this);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const response = await getData("api/scanSettings/settings");

        if (response) {
            this.setState({})
        }
        this.setState({ loading: false });
    }

    onRestartChange(e) {
        this.setState({ jobRestartMinutes: parseInt(e.target.value) });
    }

    onRetriesChange(e) {
        this.setState({ pingRetries: parseInt(e.target.value) });
    }

    onFailureChange(e) {
        this.setState({ pingFailureThreat: parseInt(e.target.value) });
    }

    async onSaveClick() {
        this.setState({ loading: true });

        const response = await postData("api/scanSettings/save", { jobRestartMinutes: this.state.jobRestartMinutes, pingRetries: this.state.pingRetries, pingFailureThreat: this.state.pingFailureThreat });
        this.setState({ loading: false });

    }

    render() {
        return (
            <div>
                <h1>Настройки сканировщика</h1>
                <div class="row">
                    <div class="col-md-6">
                        <label>Промежуток между сканированиями (мин.)</label>
                        <input class="form-control" type="number" value={this.state.jobRestartMinutes} onChange={this.onRestartChange} />
                    </div>
                    <div class="col-md-6">
                        <label>К-во повторных запросов при отсутствии ответа</label>
                        <input class="form-control" type="number" value={this.state.pingRetries} onChange={this.onRetriesChange} />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Генерировать угрозу при отсутствии ответа от источников (к-во)</label>
                        <input class="form-control" type="number" value={this.state.pingFailureThreat} onChange={this.onFailureChange} />
                    </div>
                    <div class="col-md-6">
                        <button class="btn btn-primary" onClick={this.onSaveClick}>Сохранить</button>
                    </div>
                </div>
            </div>
        );
    }
}
