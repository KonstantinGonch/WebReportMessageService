import React, { Component } from 'react';
import { DashboardActiveUsersChart } from './DashboardActiveUsersChart';
import { DashboardLastMessages } from './DashboardLastMessages';
import { DashboardMessagesChart } from './DashboardMessagesChart';
import { DashboardRegisteredThreats } from './DashboardRegisteredThreats';
import { DashboardScanRate } from './DashboardScanRate';
import './MessageList.css'

export class DashboardPage extends Component {

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
    }

    async componentDidUpdate(prevProps, prevState) {

    }


    render() {
        return (
            <main role="main" class="container pt-3">
                <div class="row d-flex justify-items-center">
                    <div class="d-flex justify-content-center pb-2">
                        <h2>Статистика</h2>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <DashboardRegisteredThreats />
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-3">
                        <DashboardLastMessages/>
                    </div>
                    <div class="col-md-9">
                        <DashboardMessagesChart />
                    </div>
                </div>
                <div class="row mt-2 mb-2">
                    <div class="col-md-6">
                        <DashboardActiveUsersChart />
                    </div>
                    <div class="col-md-6">
                        <DashboardScanRate/>
                    </div>
                </div>
            </main>
        );
    }
}
