// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { Link } from "react-router";

export const UserInfo = (props) => (
    <li className="dropdown user user-menu">
        <a href="#" className="dropdown-toggle" data-toggle="dropdown">
            <span className="hidden-xs">{props.username}</span>
        </a>
        <ul className="dropdown-menu">
            <li className="user-header">
                Obsidian Portal
                <p>
                    {props.username} - {props.level}
                    <hr/>
                    <small>{props.description}</small>
                </p>
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
        <Link to="/manage" className="logo">
            Obsidian
        </Link>
        <nav className="navbar navbar-static-top" role="navigation">
            <div className="navbar-custom-menu">
                <ul className="nav navbar-nav">
                    {props.children}
                </ul>
            </div>
        </nav>
    </header>
);

export const PortalSidebar = (props) => (
    <div className="main-sidebar">
        <div className="sidebar">
            <form action="#" method="get" className="sidebar-form">
                <div className="input-group">
                    <input type="text" name="q" className="form-control" placeholder="Search..."/>
                    <span className="input-group-btn">
                        <button type="submit" name="search" id="search-btn" className="btn btn-flat"><i className="fa fa-search"></i></button>
                    </span>
                </div>
            </form>
            <ul className="sidebar-menu">
                <li className="header">HEADER</li>
                <li className="active"><a href="#"><span>Link</span></a></li>
                <li><a href="#"><span>Another Link</span></a></li>
                <li className="treeview">
                    <a href="#"><span>Multilevel</span> <i className="fa fa-angle-left pull-right"></i></a>
                    <ul className="treeview-menu">
                        <li><a href="#">Link in level 2</a></li>
                        <li><a href="#">Link in level 2</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
);