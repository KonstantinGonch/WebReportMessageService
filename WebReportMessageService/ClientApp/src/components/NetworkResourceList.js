import React, { Component } from 'react';
import { postData, getData } from '../util.js'
import { NetworkResourceWidget } from './NetworkResourceWidget.js'
import './MessageList.css'

export class NetworkResourceList extends Component {

    constructor(props) {
        super(props);
        this.state = { networkResources: [], pageNumber: 1, pageTotal: 0, loading: false, addRowActive: false, ipAddress: '', shortName: '' };

        this.getPageNetworkResources = this.getPageNetworkResources.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
        this.onChangeIpAddress = this.onChangeIpAddress.bind(this);
        this.onChangeShortName = this.onChangeShortName.bind(this);
        this.onAddResourceButtonClick = this.onAddResourceButtonClick.bind(this);
        this.onAddButtonClick = this.onAddButtonClick.bind(this);
        this.onTrashClick = this.onTrashClick.bind(this);
    }

    async getPageNetworkResources() {
        return await getData("api/networkResource/list?pageNumber=" + this.state.pageNumber);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const pageNetworkResources = await this.getPageNetworkResources();
        this.setState({ networkResources: pageNetworkResources.networkResources, pageTotal: pageNetworkResources.totalPages, loading: false })
    }

    async componentDidUpdate(prevProps, prevState) {
        if (prevState.pageNumber !== this.state.pageNumber) {
            const pageNetworkResources = await this.getPageNetworkResources();
            this.setState({ networkResources: pageNetworkResources.networkResources, pageTotal: pageNetworkResources.totalPages, loading: false })
        }
    }

    async onPageChange(pageNumber) {
        this.setState({ loading: true, pageNumber: pageNumber });
    }

    onAddButtonClick() {
        this.setState({ addRowActive: !this.state.addRowActive, ipAddress: '', shortName: '' })
    }

    async onAddResourceButtonClick() {
        this.setState({ loading: true });
        const response = await postData("api/networkResource/save", { ipAddress: this.state.ipAddress, resourceName: this.state.shortName });
        this.setState({ loading: false, addRowActive: false });

        this.setState({ loading: true });
        const pageNetworkResources = await this.getPageNetworkResources();
        this.setState({ networkResources: pageNetworkResources.networkResources, pageTotal: pageNetworkResources.totalPages, loading: false })
    }

    onChangeIpAddress(e) {
        this.setState({ ipAddress: e.target.value })
    }

    onChangeShortName(e) {
        this.setState({ shortName: e.target.value })
    }

    async onTrashClick(recordId) {
        this.setState({ loading: true });
        const response = await postData("api/networkResource/delete", { id: recordId });

        const pageNetworkResources = await this.getPageNetworkResources();
        this.setState({ networkResources: pageNetworkResources.networkResources, pageTotal: pageNetworkResources.totalPages, loading: false })
    }

    render() {
        return (
            <>

                <main role="main" class="container pt-3">
                    <div class="d-flex justify-content-center pb-2">
                        <h2>Сканируемые ресурсы</h2>
                    </div>
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading && <div class="list-group">
                        {this.state.networkResources.map(netRes => <NetworkResourceWidget networkResource={netRes} onTrashClick={this.onTrashClick} />)}
                    </div>}
                    <button type="button" class="btn btn-primary btn-table-add" onClick={this.onAddButtonClick}>{this.state.addRowActive ? "Скрыть" : "Добавить"}</button>
                    {this.state.addRowActive &&
                        <table class="table table-borderless">
                            <thead>
                                <th></th><th></th><th></th>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <input type="text" value={this.state.ipAddress} onChange={this.onChangeIpAddress} class="form-control" placeholder="Введите IP-адрес или URL" />
                                    </td>
                                    <td>
                                        <input type="text" value={this.state.shortName} onChange={this.onChangeShortName} class="form-control" placeholder="Введите название" />
                                    </td>
                                    <td>
                                        <button class="btn btn-primary" onClick={this.onAddResourceButtonClick}>Подтвердить</button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    }
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
