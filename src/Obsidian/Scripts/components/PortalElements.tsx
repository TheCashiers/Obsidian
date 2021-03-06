﻿import * as React from "react";
import { Link } from "react-router";
import { IFormState, INamedEvent } from "../containers/FormContainer";
import { UserInfoContainer } from "../containers/UserInfoContainer";
import { styles } from "../styles/index";
import { MaterialInput } from "./Form";

interface IUserInfoState extends IFormState {
    children?: React.ReactElement<IUSerInfoEditProps>;
    onOpenModal(): void;
    onSignout(): void;

}

export const UserInfo = (props: IUserInfoState) => (
    <li className="dropdown user user-menu">
        <a href="#" className="dropdown-toggle" data-toggle="dropdown">
            <span>{props.surnName} {props.givenName}</span>
        </a>
        <ul className="dropdown-menu">
            <li style={styles.userCard}>
                <h2>
                    Hi {props.surnName} {props.givenName}!
                    <br />

                    <small>Administrator</small>
                </h2>
            </li>
            <li className="user-footer">
                <div className="pull-left">
                    <button onClick={props.onOpenModal} className="btn btn-default btn-flat">Profile</button>
                </div>
                <div className="pull-right">
                    <button onClick={props.onSignout} className="btn btn-default btn-flat">Sign out</button>
                </div>
            </li>
        </ul>
        {props.children}
    </li>
);

interface IUSerInfoEditProps extends IFormState {
    shouldDisplay: boolean;
    onCloseModal(): void;
    onInputChange(e: INamedEvent): void;
    onSubmit(e: React.MouseEvent<HTMLInputElement>): void;
}

export const UserInfoEdit = (props: IUSerInfoEditProps) => (
    <div className="modal" style={{ display: props.shouldDisplay ? "block" : "none" }}>
        <div className = "modal-dialog">
            <div className = "modal-content">
                <div className = "modal-header">
                    <button onClick={props.onCloseModal} type = "button" className = "close"> × </button>
                    <h2 className="modal-title" style={styles.modalCard}> Edit your personal information </h2>
                </div>
                <div className="modal-body" style={styles.modalBody}>
                    <MaterialInput
                        name="emailAddress"
                        label="Email Addr"
                        onInputChange={props.onInputChange}
                        value={props.emailAddress}
                        placeholder="email..."
                        type="text"
                    />
                    <MaterialInput
                        name="gender"
                        label="gender"
                        onInputChange={props.onInputChange}
                        value={props.gender}
                        placeholder="gender..."
                        type="text"
                    />
                    <MaterialInput
                        name="givenName"
                        label="givenname"
                        onInputChange={props.onInputChange}
                        value={props.givenName}
                        placeholder="givenname..."
                        type="text"
                    />
                    <MaterialInput
                        name="surnName"
                        label="surnname"
                        onInputChange={props.onInputChange}
                        value={props.surnName}
                        placeholder="surnname..."
                        type="text"
                    />
                </div>
                <div className = "modal-footer">
                    <button onClick={props.onCloseModal} type = "button" className = "btn btn-default"> Close </button>
                    <button onClick={props.onSubmit} type="button" className="btn btn-primary"> Save changes </button>
                </div>
            </div>
        </div>
    </div>
);
interface IPortalHeaderProps {
    token?: string;
    filter: string;
    handleFilterChange(e: INamedEvent): void;
}
export const PortalHeader = (props: IPortalHeaderProps) => (
    <header className="main-header">
        <nav className="navbar navbar-static-top">
            <div className="container-fluid">
                <div className="navbar-header">
                    <Link to="/manage" className="navbar-brand">Obsidian</Link>
                    <button
                        type="button"
                        className="navbar-toggle collapsed"
                        data-toggle="collapse"
                        data-target="#navbar-collapse"
                    >
                        <i className="fa fa-bars" />
                    </button>
                </div>
                <div className="collapse navbar-collapse" id="navbar-collapse">
                    <ul className="nav navbar-nav">
                        <li><Link to="/manage/users">Users</Link></li>
                        <li><Link to="/manage/clients">Clients</Link></li>
                        <li><Link to="/manage/scopes">Scopes</Link></li>
                    </ul>
                    <SearchBox handleFilterChange={props.handleFilterChange} filter={props.filter}/>
                    <ul className="nav navbar-nav navbar-right">
                        <UserInfoContainer token={props.token} />
                    </ul>
                </div>
            </div>
        </nav>
    </header>);

const SearchBox = (props: IPortalHeaderProps) =>
    (
        <form className="navbar-form navbar-left" role="search">
            <div className="form-group">
                <input
                    type="text"
                    name="filter"
                    onChange={props.handleFilterChange}
                    value={props.filter}
                    className="form-control"
                    id="navbar-search-input"
                    placeholder="Search"
                />
            </div>
        </form>
    );

interface IComboBoxProps {
    name: string;
    onSelectChange(): void;
}

const Combobox = (props: IComboBoxProps) =>
    (
        <label style={styles.comboBox}>
            <input type="checkbox" onChange={props.onSelectChange} />
            {props.name}
        </label>
    )
;
