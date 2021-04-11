import React from "react";
import RootStore from "../../Stores/RootStore";
import {inject, observer} from "mobx-react";
import {Dropdown, DropdownItem, DropdownMenu, MenuItem} from "semantic-ui-react";
import LoginModal from "../../Modals/LoginModal/LoginModal";
import AuthenticationStore from "../../Stores/AuthenticationStore";

export interface AccountMenuProps {
    rootStore?: RootStore
}

class AccountMenu extends React.Component<AccountMenuProps> {
    private authStore: AuthenticationStore;

    public constructor(props: AccountMenuProps) {
        super(props);
        this.authStore = this.props.rootStore?.authenticationStore!;
    }

    public render() {
        return (
            <React.Fragment>
                {
                    !this.authStore.isAuthenticated ?
                        <>
                            <LoginModal
                                trigger={
                                    <MenuItem as="a">
                                        Login
                                    </MenuItem>
                                }
                            />
                            <MenuItem as="a">
                                Register
                            </MenuItem>
                        </>
                        :
                        <>
                            <Dropdown item text={this.authStore.user?.login!}>
                                <DropdownMenu>
                                    <DropdownItem text="Logout" onClick={this.logoutHandler}/>
                                </DropdownMenu>
                            </Dropdown>
                        </>
                }
            </React.Fragment>
        );
    }

    public logoutHandler = async () => {
        await this.authStore.logout();
    }
}

export default inject("rootStore")(observer(AccountMenu));