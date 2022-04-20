import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { MessageList } from './components/MessageList'
import './custom.css'
import { NetworkResourceList } from './components/NetworkResourceList';
import { ScanResultList } from './components/ScanResultList';
import { ScanSettings } from './components/ScanSettings';
import { ThreatPage } from './components/ThreatPage';
import { MonitorAbonentList } from './components/MonitorAbonentList';
import { MonitorAbonentPage } from './components/MonitorAbonentPage';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/messages' component={MessageList} />
                <Route path='/scanResources' component={NetworkResourceList} />
                <Route path='/scanJobResults' component={ScanResultList} />
                <Route path='/scanJobSettings' component={ScanSettings} />
                <Route path='/scanJobSettings' component={ScanSettings} />
                <Route path='/threat/:id' component={ThreatPage} />
                <Route path='/monitorAbonents' component={MonitorAbonentList} />
                <Route path='/monitorAbonent/:id' component={MonitorAbonentPage} />
            </Layout>
        );
    }
}
