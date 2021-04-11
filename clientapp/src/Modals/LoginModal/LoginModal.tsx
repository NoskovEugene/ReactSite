import React from "react";
import {Button, Form, FormGroup, FormInput, Icon, Modal, ModalContent, ModalHeader} from "semantic-ui-react";
import {inject, observer} from "mobx-react";
import RootStore from "../../Stores/RootStore";
import LoginModalStore from "./LoginModalStore";
import AuthenticationStore from "../../Stores/AuthenticationStore";

export interface LoginModalProps {
    rootStore?: RootStore,
    trigger: React.ReactNode
}


class LoginModal extends React.Component<LoginModalProps> {
    private store: LoginModalStore
    private authStore: AuthenticationStore;


    public constructor(props: LoginModalProps) {
        super(props);
        this.store = new LoginModalStore();
        this.authStore = this.props.rootStore?.authenticationStore!;
    }

    public render() {
        return (
            <Modal trigger={this.props.trigger}
                   size="small"
                   open={this.store.isOpen}
                   onClose={() => this.store.isOpen = false}
                   onOpen={() => this.store.isOpen = true}
                   onSubmit={()=> this.submitHandler()}
                   dimmer="inverted"
            >
                <ModalHeader>
                    Login
                </ModalHeader>
                <ModalContent>
                    <Form>
                        <FormInput
                            placeholder="Login"
                            icon={{name: "user", circular: true}}
                            required
                            onChange={(event, data) => {
                                this.store.login = data.value
                            }}
                        />
                        <FormInput
                            placeholder="Password"
                            icon=
                                {
                                    {
                                        name: this.store.passwordVisibility ? "eye slash" : "eye",
                                        circular: true,
                                        onClick: this.showPassHandler,
                                        link: true
                                    }
                                }
                            type={this.store.passwordVisibility ? "text" : "password"}
                            onChange={(event, data) => {
                                this.store.password = data.value
                            }}
                            required
                        />
                        <FormGroup widths="equal" inline>
                            <Button fluid color="green">
                                <Icon name="checkmark"/>
                                Login
                            </Button>
                            <Button fluid onClick={() => this.store.isOpen = false}>
                                <Icon name="remove"/>
                                Close
                            </Button>
                        </FormGroup>
                    </Form>
                </ModalContent>
            </Modal>
        );
    }

    public showPassHandler = () => {
        this.store.passwordVisibility = !this.store.passwordVisibility;
    }

    public submitHandler = async () => {
         await this.authStore.authenticate(this.store.login,this.store.password);
         this.store.isOpen = false;
    }

}

export default inject("rootStore")(observer(LoginModal));