import React, { Component } from 'react';
import { getData } from '../util.js'
import { ScanJobResultWidget } from './ScanJobResultWidget.js'
import './MessageList.css'

export class ScanResultList extends Component {

    constructor(props) {
        super(props);
        this.state = { scanJobResults: [], pageNumber: 1, pageTotal: 0, loading: false };

        this.getPageJobResults = this.getPageJobResults.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
    }

    async getPageJobResults() {
        return await getData("api/scanJob/list?pageNumber=" + this.state.pageNumber);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const pageJobResults = await this.getPageJobResults();
        this.setState({ jobResults: pageJobResults.scanJobResults, pageTotal: pageJobResults.totalPages, loading: false })
    }

    async componentDidUpdate(prevProps, prevState) {
        if (prevState.pageNumber !== this.state.pageNumber) {
            const pageJobResults = await this.getPageJobResults();
            this.setState({ jobResults: pageJobResults.scanJobResults, pageTotal: pageJobResults.totalPages, loading: false })
        }
    }

    onPageChange(pageNumber) {
        this.setState({ loading: true, pageNumber: pageNumber });
    }

    render() {
        return (
            <>
                <main role="main" class="container">
                    <div class="list-group">
                        {this.state.scanJobResults.map(result => <ScanJobResultWidget result={result} />)}
                    </div>
                    <a class="nav-link" href="/scanJobSettings">Настройки</a>
                </main>
                <nav aria-label="..." class="pagination-container">
                    <ul class="pagination">
                        <li className={"page-item" + this.state.pageNumber === 1 ? " disabled" : ""}>
                            <a class="page-link" onClick={e => this.onPageChange(this.state.pageNumber - 1)}>Назад</a>
                        </li>
                        <li className={"page-item" + this.state.pageNumber === this.state.pageTotal ? " disabled" : ""}>
                            <a class="page-link" onClick={e => this.onPageChange(this.state.pageNumber + 1)} >Вперед</a>
                        </li>
                    </ul>
                </nav>
            </>
        );
    }
}
