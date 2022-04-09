import React, { Component } from 'react';

export class ScanJobResultWidget extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        var scanDate = new Date(this.props.result.scanDate);
        var nextScanDate = new Date(this.props.result.planNextScan);
        return (
            <div class="list-group-item list-group-item-action flex-column align-items-start">
                <div class="row">
                    <div class="col-md-6">
                        <label>Дата сканирования:</label>
                        <p>{scanDate.toLocaleString()}</p>
                    </div>
                    <div class="col-md-6">
                        <label>Следующее сканирование:</label>
                        <p>{nextScanDate.toLocaleString()}</p>
                    </div>
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
