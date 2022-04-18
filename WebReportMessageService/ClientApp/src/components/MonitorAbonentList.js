import React, { Component } from 'react';
import { getData } from '../util.js'
import { ScanJobResultWidget } from './ScanJobResultWidget.js'
import './MessageList.css'
import { MonitorAbonentWidget } from './MonitorAbonentWidget.js';

export class MonitorAbonentList extends Component {

    constructor(props) {
        super(props);
        this.state = { monitorAbonents: [], pageNumber: 1, pageTotal: 0, loading: false };

        this.getPageMonitorAbonents = this.getPageMonitorAbonents.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
    }

    async getPageMonitorAbonents() {
        return await getData("api/monitorAbonent/list?pageNumber=" + this.state.pageNumber);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const pageMonitorAbonents = await this.getPageMonitorAbonents();
        this.setState({ monitorAbonents: pageMonitorAbonents.monitorAbonents, pageTotal: pageMonitorAbonents.totalPages, loading: false })
    }

    async componentDidUpdate(prevProps, prevState) {
        if (prevState.pageNumber !== this.state.pageNumber) {
            const pageMonitorAbonents = await this.getPageMonitorAbonents();
            this.setState({ monitorAbonents: pageMonitorAbonents.monitorAbonents, pageTotal: pageMonitorAbonents.totalPages, loading: false })
        }
    }

    onPageChange(pageNumber) {
        this.setState({ loading: true, pageNumber: pageNumber });
    }

    render() {
        return (
            <>
                <main role="main" class="container">
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading && <div class="list-group">
                        {this.state.monitorAbonents.map(monitorAbonent => <MonitorAbonentWidget abonent={monitorAbonent} />)}
                    </div>}
                    <a class="nav-link" href="/scanJobSettings">Настройки</a>
                </main>
                <nav aria-label="..." class="pagination-container">
                    <ul class="pagination">
                        <li className={"page-item" + (this.state.pageNumber === 1 ? " disabled" : "")}>
                            <a class="page-link" onClick={e => this.onPageChange(this.state.pageNumber - 1)}>Назад</a>
                        </li>
                        <li className={"page-item" + (this.state.pageNumber === this.state.pageTotal ? " disabled" : "")}>
                            <a class="page-link" onClick={e => this.onPageChange(this.state.pageNumber + 1)} >Вперед</a>
                        </li>
                    </ul>
                </nav>
            </>
        );
    }
}
