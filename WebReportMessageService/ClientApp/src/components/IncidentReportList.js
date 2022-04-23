import React, { Component } from 'react';
import { getData } from '../util.js'
import { IncidentReportWidget } from './IncidentReportWidget.js'
import './MessageList.css'

export class IncidentReportList extends Component {

    constructor(props) {
        super(props);
        this.state = { reports: [], pageNumber: 1, pageTotal: 0, loading: false };

        this.getPageReports = this.getPageReports.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
    }

    async getPageReports() {
        return await getData("api/incident/list?pageNumber=" + this.state.pageNumber);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const pageReports = await this.getPageReports();
        this.setState({ reports: pageReports.incidentReports, pageTotal: pageReports.totalPages, loading: false })
    }

    async componentDidUpdate(prevProps, prevState) {
        if (prevState.pageNumber !== this.state.pageNumber) {
            const pageReports = await this.getPageReports();
            this.setState({ reports: pageReports.incidentReports, pageTotal: pageReports.totalPages, loading: false })
        }
    }

    onPageChange(pageNumber) {
        this.setState({ loading: true, pageNumber: pageNumber });
    }

    render() {
        return (
            <>
                <main role="main" class="container pt-3">
                    <div class="d-flex justify-content-center pb-2">
                        <h2>Список инцидентов</h2>
                    </div>
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading && <div class="list-group">
                        {this.state.reports.map(report => <IncidentReportWidget report={report} />)}
                    </div>}
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
