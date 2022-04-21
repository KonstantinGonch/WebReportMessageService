import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div class="d-flex flex-row">
                <div class="">
                    <NavMenu />
                </div>
                <div class="w-100 d-flex justify-content-center">
                    <Container>
                        {this.props.children}
                    </Container>
                </div>
            </div>
        );
    }
}
