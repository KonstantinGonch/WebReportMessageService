import React, { Component } from 'react';

export class MessageWidget extends Component {
    static displayName = MessageWidget.name;

    constructor(props) {
        super(props);
    }

    render() {
        var d = new Date(this.props.message.messageDate);
        return (
            <div class={`list-group-item ${this.props.message.messageType == 1 ? "list-group-item-danger" : (this.props.message.messageType == 2 ? "list-group-item-warning" : "list-group-item-action")} flex-column align-items-start`}>
                <div class="d-flex w-100 justify-content-between">
                    <h5>{d.toLocaleString()}</h5>
                </div>
                <p class="mb-1">{ this.props.message.content }</p>
            </div>
        );
    }
}
