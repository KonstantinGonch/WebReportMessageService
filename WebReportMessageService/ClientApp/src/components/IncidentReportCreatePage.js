import React, { Component } from 'react';
import { postData, getData } from '../util.js'

export class IncidentReportCreatePage extends Component {

    constructor(props) {
        super(props);
        this.state = { loading: false, attackVector: 0, typeOfAttack: 0, assistanceNeeded: false, description: "", threat: null };

        this.onChangeAttackVector = this.onChangeAttackVector.bind(this);
        this.onChangeAttackType = this.onChangeAttackType.bind(this);
        this.onChangeAssistanceNeeded = this.onChangeAssistanceNeeded.bind(this);
        this.onChangeDescription = this.onChangeDescription.bind(this);
        this.onSubmitForm = this.onSubmitForm.bind(this);
    }

    async componentDidMount() {
        this.setState({ loading: true });
        let threatId = this.props.match.params.threatId;
        let threatResponse = await getData("/api/threat/get?id=" + threatId);

        this.setState({ loading: false, threat: threatResponse });
    }

    async onSubmitForm(e) {
        e.preventDefault();
        this.setState({ loading: true });

        let saveResponse = await postData("/api/incident/save", { description: this.state.description, assistanceNeeded: !!this.state.assistanceNeeded, typeOfAttack: Number(this.state.typeOfAttack), attackVector: Number(this.state.attackVector) });
        this.setState({ loading: false });
        alert("Обращение сохранено");
    }

    onChangeAttackVector(e) {
        this.setState({ attackVector: e.target.value })
    }

    onChangeAttackType(e) {
        this.setState({ typeOfAttack: e.target.value })
    }

    onChangeAssistanceNeeded(e) {
        this.setState({ assistanceNeeded: e.target.value })
    }

    onChangeDescription(e) {
        this.setState({ description: e.target.value })
    }

    render() {
        return (
            <main role="main" class="container">
                {this.state.loading &&
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-primary list-spinner" role="status">
                            <span class="sr-only">Загрузка...</span>
                        </div>
                    </div>}
                {!this.state.loading &&
                    <div class="form-group form-floating">
                        <div class="row d-flex justify-content-center">
                            <h1>Создание обращения об инциденте в ФинЦЕРТ</h1>
                        </div>
                        <div class="row d-flex justify-content-center">
                            <h2>Зарегистрированное событие безопасности:</h2>
                        </div>
                        <div class="row d-flex justify-content-center">
                            {this.state.threat && <p>{this.state.threat.threatMessage}</p>}
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="floatingVector">Вектор атаки</label>
                                <select class="form-control" id="floatingVector" value={this.state.attackVector} onChange={this.onChangeAttackVector}>
                                    <option key={0} value={0}>INT (направлено на инфраструктуру)</option>
                                    <option key={1} value={1}>EXT (направлено на клиентов)</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label for="floatingAttackType">Вектор атаки</label>
                                <select class="form-control" id="floatingAttackType" value={this.state.typeOfAttack} onChange={this.onChangeAttackType}>
                                    <option key={0} value={0}>Изменение маршрутно-адресной информации</option>
                                    <option key={1} value={1}>Использование вредоносного программного обеспечения</option>
                                    <option key={2} value={2}>Использование методов социальной инженерии</option>
                                    <option key={3} value={3}>Реализация атаки "отказ в обслуживании"</option>
                                    <option key={4} value={4}>Несанкционированный доступ к банкомату</option>
                                    <option key={5} value={5}>Эксплуатация уязвимостей</option>
                                    <option key={6} value={6}>Компрометация учетных данных</option>
                                    <option key={7} value={7}>Реализация спам-рассылки</option>
                                    <option key={8} value={8}>Взаимодействие с ботнет-сетями</option>
                                    <option key={8} value={8}>Смена IMEI,IMSI</option>
                                    <option key={9} value={9}>Использование фишинговых ресурсов</option>
                                    <option key={10} value={10}>Размещение запрещенного контента</option>
                                    <option key={11} value={11}>Размещение вредоносного ресурса</option>
                                    <option key={12} value={12}>Выполение изменения контента</option>
                                    <option key={13} value={13}>Выполение сканирования портов</option>
                                    <option key={14} value={14}>Иное</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="floatingAssistance">Необходимость содействия ФинЦЕРТ</label>
                                <select class="form-control" id="floatingAssistance" value={this.state.assistanceNeeded} onChange={this.onChangeAssistanceNeeded}>
                                    <option key={0} value={0}>Нет</option>
                                    <option key={1} value={1}>Да</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="floatingDescription">Содержание обращения</label>
                                <textarea class="form-control" id="floatingDescription" rows="5" value={this.state.description} onChange={this.onChangeDescription}></textarea>
                            </div>
                        </div>
                        <div class="row d-flex justify-content-center mt-2">
                            <button className="btn btn-primary" type="submit" onClick={this.onSubmitForm} disabled={this.state.loading}>
                                {this.state.loading && <div class="spinner-border text-light" role="status">
                                    <span class="sr-only">Загрузка...</span>
                                </div>}
                                {!this.state.loading && "Отправить"}
                            </button>
                        </div>
                    </div>}
            </main>
        );
    }
}
