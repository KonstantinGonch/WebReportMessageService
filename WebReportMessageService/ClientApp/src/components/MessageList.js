import React, { Component } from 'react';
import { getData } from '../util.js'
import { MessageWidget } from './MessageWidget.js'
import './MessageList.css'

export class MessageList extends Component {
    static displayName = MessageList.name;

    constructor(props) {
        super(props);
        this.state = { messages: [], pageNumber: 1, pageTotal: 0, loading: false };

        this.getPageMessages = this.getPageMessages.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
    }

    async getPageMessages() {
        return await getData("api/message/list?pageNumber=" + this.state.pageNumber);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const pageMessages = await this.getPageMessages();
        this.setState({ messages: pageMessages.messages, pageTotal: pageMessages.totalPages, loading: false })
    }

    async componentDidUpdate(prevProps, prevState) {
        if (prevState.pageNumber !== this.state.pageNumber) {
            const pageMessages = await this.getPageMessages();
            this.setState({ messages: pageMessages.messages, pageTotal: pageMessages.totalPages, loading: false })
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
                        <h2>Список сообщений</h2>
                    </div>
                    {this.state.loading &&
                        <div class="d-flex justify-content-center">
                            <div class="spinner-border text-primary list-spinner" role="status">
                                <span class="sr-only">Загрузка...</span>
                            </div>
                        </div>}
                    {!this.state.loading && <div class="list-group">
                        {this.state.messages.map(message => <MessageWidget message={message} />)}
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
