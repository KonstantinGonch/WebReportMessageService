import React, { Component } from 'react';

export class ScanJobResultWidget extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        return (
            <div class="list-group-item list-group-item-action flex-column align-items-start">
                <div class="d-flex w-100 justify-content-between">
                </div>
                <p class="mb-1">{this.props.scanDate.resourceName}</p>
            </div>
        );
    }
}
