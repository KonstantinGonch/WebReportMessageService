import React, { Component } from 'react';

export class IncidentReportWidget extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        var d = new Date(this.props.report.fixationAt);
        return (
            <div class={`list-group-item list-group-item-action flex-column align-items-start`}>
                <div class="row">
                    <div class="col-md-6">
                        <p>Дата регистрации: {d.toLocaleString()}</p>
                    </div>
                    <div class="col-md-6">
                        <p>Необходимость содействия ФинЦЕРТ: {this.props.report.assistanceNeeded ? "Да" : "Нет"}</p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <p>Содержание: {this.props.report.description}</p>
                    </div>
                </div>
            </div>
        );
    }
}
