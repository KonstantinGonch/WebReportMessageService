import React, { Component } from 'react';

export class ScanJobResultWidget extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        var scanDate = new Date(this.props.result.scanDate);
        var threatId = this.props.result.threatId;
        var nextScanDate = new Date(this.props.result.planNextScan);
        return (
            <div class={`list-group-item ${threatId ? "list-group-item-danger" : "list-group-item-success"} flex-column align-items-start`}>
                <div class="row">
                    <div class="col-md-6">
                        <label>Дата сканирования:</label>
                        <p>{scanDate.toLocaleString()}</p>
                    </div>
                    <div class="col-md-4">
                        <label>Следующее сканирование:</label>
                        <p>{nextScanDate.toLocaleString()}</p>
                    </div>
                    {!!threatId &&
                        <div class="col-md-2">
                            <a href={`threat/${threatId}`} class="badge badge-danger">Зарегистрирована угроза</a>
                        </div>}
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Результат сканирования:</label>
                        <p>{this.props.result.successScanned} из {this.props.result.totalResources}</p>
                    </div>
                </div>
            </div>
        );
    }
}
