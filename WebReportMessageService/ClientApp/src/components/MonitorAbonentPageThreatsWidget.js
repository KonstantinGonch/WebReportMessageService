import React, { Component } from 'react';

export class MonitorAbonentPageThreatsWidget extends Component {

    constructor(props) {
        super(props);
    }

    async componentDidMount() {

    }

    render() {
        return (
            <ul class="list-group">
                {this.props.threats.map(threat => 
                    <li class="list-group-item list-group-item-danger">{threat.threatMessage}</li>
                )}
            </ul>
        );
    }
}
