import React, { Component } from 'react';
import { postData } from '../util.js'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { messageContent: '', messageType: 0, loading: false, error: null };

        this.onSubmitForm = this.onSubmitForm.bind(this);
        this.onChangeMessageContent = this.onChangeMessageContent.bind(this);
        this.onChangeMessageType = this.onChangeMessageType.bind(this);
    }

    async onSubmitForm(e) {
        e.preventDefault();

        this.setState({ loading: true });
        const response = await postData("api/message/save", { content: this.state.messageContent, messageType: this.state.messageType })

        this.setState({ loading: false });
        alert("Сообщение сохранено");
    }

    onChangeMessageContent(e) {
        this.setState({messageContent: e.target.value})
    }

    onChangeMessageType(e) {
        this.setState({ messageType: parseInt(e.target.value) })
    }

    render() {
        return (
            <main role="main" class="container pt-5">
                <div class="starter-template">
                    <h1>Введите содержание и категорию сообщения</h1>
                    <form onSubmit={this.onSubmitForm}>
                        <div class="form-group">
                            <label for="exampleFormControlTextarea1">Содержание обращения</label>
                            <textarea disabled={this.state.loading} class="form-control" id="exampleFormControlTextarea1" rows="5" value={this.state.messageContent} onChange={this.onChangeMessageContent}></textarea>
                        </div>
                        <div class="form-group">
                            <label for="exampleFormControlSelect1">Категория</label>
                            <select disabled={this.state.loading} class="form-control" id="exampleFormControlSelect1" value={this.state.messageType} onChange={this.onChangeMessageType}>
                                <option key={0} value={0}>Неформализованное обращение</option>
                                <option key={1} value={1}>Предполагаемая угроза</option>
                                <option key={2} value={2}>Обратная связь</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <button className="btn btn-primary" type="submit" disabled={this.state.loading}>
                                {this.state.loading && <div class="spinner-border text-light" role="status">
                                    <span class="sr-only">Загрузка...</span>
                                </div>}
                                {!this.state.loading && "Отправить"}
                            </button>
                        </div>
                    </form>
                </div>
            </main>
        );
    }
}
