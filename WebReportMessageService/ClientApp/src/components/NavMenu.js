import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import {
    CDBSidebar,
    CDBSidebarContent,
    CDBSidebarHeader,
    CDBSidebarMenu,
    CDBSidebarMenuItem,
} from 'cdbreact';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <div style={{ display: 'flex', height: '100vh', overflow: 'scroll initial' }}>
                <CDBSidebar textColor="#fff" backgroundColor="#333">
                    <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large"></i>}>
                        <a href="/" className="text-decoration-none" style={{ color: 'inherit' }}>
                            Меню навигации
                        </a>
                    </CDBSidebarHeader>

                    <CDBSidebarContent className="sidebar-content">
                        <CDBSidebarMenu>
                            <NavLink exact href="/" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="columns">Главная</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink href="/sendMessage" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="comment">Ввод сообщения</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink href="/messages" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="envelope">Список сообщений</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink href="/scanResources" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="list">Сканируемые ресурсы</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink href="/scanJobResults" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="check-square">Результаты сканирования</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink href="/monitorAbonents" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="user">Пользователи Монитора</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink href="/reports" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="exclamation">Инциденты</CDBSidebarMenuItem>
                            </NavLink>
                        </CDBSidebarMenu>
                    </CDBSidebarContent>
                </CDBSidebar>
            </div>
        );
    }
}
