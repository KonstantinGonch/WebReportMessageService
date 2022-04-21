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
                    <li class="list-group-item list-group-item-danger"><a href={`threat/${threat.id}`} class="link-dark">
                        {threat.threatMessage}</a></li>
                )}
            </ul>
        );
    }
}
