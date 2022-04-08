import React, { Component } from 'react';

export class NetworkResourceWidget extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        return (
            <div class="list-group-item list-group-item-action flex-column align-items-start">
                <div class="d-flex w-100 justify-content-between">
                    <h5>{this.props.networkResource.ipAddress}</h5>
                </div>
                <p class="mb-1">{this.props.networkResource.resourceName}</p>
                <button class="btn btn-danger" onClick={e => this.props.onTrashClick(this.props.networkResource.id)}>Удалить</button>
            </div>
        );
    }
}
