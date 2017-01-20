import * as React from "react";
import { Link } from "react-router";
import { UserInfoContainer } from "../containers/UserInfoContainer"
import { styles } from "../styles"

export const UserInfo = (props) => (
    <li className="dropdown user user-menu">
        <a href="#" className="dropdown-toggle" data-toggle="dropdown">
            <span className="hidden-xs">{props.username}</span>
        </a>
        <ul className="dropdown-menu ">
            <li style={styles.userCard}>
                <h2>
                    {props.username} - {props.level}
                    <br/>

                    <small>{props.description}</small>
                </h2>
            </li>
            <li className="user-footer">
                <div className="pull-left">
                    <a href="#" className="btn btn-default btn-flat">Profile</a>
                </div>
                <div className="pull-right">
                    <a href="#" className="btn btn-default btn-flat">Sign out</a>
                </div>
            </li>
        </ul>
    </li>
)
export const PortalHeader = (props) => (
    <header className="main-header">
        <nav className="navbar navbar-static-top">
            <div className="container-fluid">
                <div className="navbar-header">
                    <Link to="/manage" className="navbar-brand">Obsidian</Link>
                    <button type="button" className="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse">
                        <i className="fa fa-bars"/>
                    </button>
                </div>
                <div className="collapse navbar-collapse" id="navbar-collapse">
                    <ul className="nav navbar-nav">
                        <li><Link to="/manage/users">Users</Link></li>
                        <li><Link to="/manage/clients">Clients</Link></li>
                        <li><a href="#">Scopes</a></li>
                        <li><a href="#">Claims</a></li>
                        <li className="dropdown">
                            <a href="#" className="dropdown-toggle" data-toggle="dropdown">Dropdown <span className="caret"></span></a>
                            <ul className="dropdown-menu" role="menu">
                                <li><a href="#">Action</a></li>
                                <li><a href="#">Another action</a></li>
                                <li><a href="#">Something else here</a></li>
                                <li className="divider"></li>
                                <li><a href="#">Separated link</a></li>
                                <li className="divider"></li>
                                <li><a href="#">One more separated link</a></li>
                            </ul>
                        </li>
                    </ul>
                    <form className="navbar-form navbar-left" role="search">
                        <div className="form-group">
                            <input type="text" className="form-control" id="navbar-search-input" placeholder="Search"/>
                        </div>
                    </form>
                    <ul className="nav navbar-nav navbar-right">
                        <UserInfoContainer/>
                    </ul>
                </div>
            </div>
        </nav>
    </header>);