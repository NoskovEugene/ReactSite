import React from 'react';
import 'semantic-ui-css/semantic.min.css'
import {observer, Provider} from "mobx-react";
import RootStore from "./Stores/RootStore";
import {History} from 'history';
import AuthenticationStore from "./Stores/AuthenticationStore";
import {Helmet} from 'react-helmet';
import {Icon, Menu, MenuItem, MenuMenu} from "semantic-ui-react";
import AccountMenu from "./Components/AccountComponent/AccountMenu";


export interface AppProps {
  rootStore: RootStore
  history: History
}


class App extends React.Component<AppProps>{

  private authStore: AuthenticationStore;

  constructor(props: AppProps) {
    super(props);
    this.authStore = props.rootStore.authenticationStore;
  }

  public render() {
    return (
        <Provider rootStore={this.props.rootStore} history={this.props.history}>
          <Helmet title={this.props.rootStore.metadataStore.title}>
          </Helmet>
          <Menu>
            <MenuItem as="a">
              <Icon name="500px" size="big"/>
            </MenuItem>
            <MenuMenu position="right">
              <AccountMenu/>
            </MenuMenu>
          </Menu>
        </Provider>
    )
  }
}



export default observer(App);
