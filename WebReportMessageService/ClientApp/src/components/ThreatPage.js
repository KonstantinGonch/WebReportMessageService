import React, { Component } from 'react';
import { getData } from '../util';

export class ThreatPage extends Component {

    constructor(props) {
        super(props);
        this.state = { threat: null, loading: false };
    }

    async componentDidMount() {
        this.setState({ loading: true });
        let id = this.props.match.params.id;
        let response = await getData("/api/threat/get?id=" + id);

        this.setState({ threat: response, loading: false });
    }

    render() {
        var date = new Date(this.state.threat?.dateAppeared);
        return (
            <main role="main" class="container">
                {this.state.loading &&
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-primary list-spinner" role="status">
                            <span class="sr-only">Загрузка...</span>
                        </div>
                    </div>}
                {!this.state.loading &&
                    <>
                        <div class="row">
                            <h1>Время возникновения угрозы:</h1>
                        </div>
                        <div class="row">
                            {this.state.threat && <h5>{date.toLocaleString()}</h5>}
                        </div>
                        <div class="row">
                            <h1>Причина возникновения:</h1>
                        </div>
                        <div class="row">
                            {this.state.threat && <h5>{this.state.threat.threatMessage}</h5>}
                        </div>
                    </>}
            </main>
        );
    }
}
