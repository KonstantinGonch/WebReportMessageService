import React, { Component } from 'react';

export class MonitorAbonentWidget extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        var lastSyncDate = new Date(this.props.abonent.lastSync);
        return (
            <div class="list-group-item list-group-item-action flex-column align-items-start">
                <a href={`monitorAbonent/${this.props.abonent.id}`}>
                <div class="row">
                    <div class="col-md-6">
                        <h3>{ this.props.abonent.hostName}</h3>
                    </div>
                    <div class="col-md-6">
                        <h5>Последняя синхронизация: </h5> <p>{lastSyncDate.toLocaleString()}</p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <h5>Событий безопасности: </h5> <h6>{this.props.abonent.attentionEvents}</h6>
                    </div>
                    </div>
                    </a>
            </div>
        );
    }
}
