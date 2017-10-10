import * as React from "react";
import {UserInfo, UserInfoEdit} from "../components/PortalElements";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import {FormContainer} from "./FormContainer";

export class UserInfoContainer extends FormContainer {
    public handleClose: () => void;
    public handleOpen: () => void;
    constructor(props) {
        super(props);
        this.state = { emailAddress: "", gender: "", givenName: "", surnName: "Admin", editing: false };
        this.handleSignout = this.handleSignout.bind(this);
        this.handleClose = this.handleChangeEditState.bind(this, false);
        this.handleOpen = this.handleChangeEditState.bind(this, true);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public async componentWillMount() {

        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getProfile.request_uri);
            if (response.data.emailAddress == null) {
                // alert user to finish personal information.
                this.handleChangeEditState(true);
            } else {
                this.setState(response.data);
            }
        } catch (error) {
            this.setState({ surnName: "error" });
        }
    }

    public async handleSubmit(e) {
        e.preventDefault();
        const emailAddress: string = this.state.emailAddress;
        const gender: string = this.state.gender;
        const givenName: string = this.state.givenName;
        const surnName: string = this.state.surnName;
        if (emailAddress && givenName && surnName) {
            try {
                const payload = { emailAddress, gender, givenName, surnName };
                await axios.getAxios(this.props.token)
                    .put(api.configs.editProfile.request_uri, payload);
            } catch (error) {
                this.setState({ surnName: "error occured." });
            } finally {
                this.setState({ editing: false });
            }
        } else { return; }
    }

    public async handleSignout() {
        await axios.getAxios(this.props.token).get(api.configs.signOut.request_uri);
        location.reload();
    }
    public handleChangeEditState(state: boolean) {
        this.setState({ editing: state });
    }
    public render() {
        return (
                <UserInfo
                    onOpenModal={this.handleOpen}
                    surnName={this.state.surnName}
                    givenName={this.state.givenName}
                    onSignout={this.handleSignout}
                >
                    <UserInfoEdit
                        onInputChange={this.handleInputChange}
                        emailAddress={this.state.emailAddress}
                        gender={this.state.gender}
                        surnName={this.state.surnName}
                        givenName={this.state.givenName}
                        shouldDisplay={this.state.editing}
                        onCloseModal={this.handleClose}
                        onSubmit={this.handleSubmit}
                    />
                </UserInfo>
        );
    }
}
