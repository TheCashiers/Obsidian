import * as React from "react";
import { Motion, spring } from "react-motion";
import { Link } from "react-router";

export interface IListItem {
    displayName?: string;
    userName?: string;
    scopeName?: string;
    id: string;
}

export interface IListProps {
    createLink?: string;
    action?: string;
    editLink?: string;
    icon?: string;
    items?: IListItem[];
    name?: string;
    id?: string;
}
export const List = (props: IListProps) => (
    <div className="content-wrapper content">
        <Link to={props.createLink}>
            <button className="btn btn-lg btn-success">{props.action}...</button>
        </Link>
        <div className="list-group">
            {props.items.map((item: IListItem, index: number) =>
                (
                    <Item
                        name={item.displayName || item.userName || item.scopeName}
                        id={item.id}
                        key={item.id}
                        editLink={props.editLink}
                        icon={props.icon}
                    />))}
        </div>
    </div>
);

export const Item = (props: IListProps) => (
    <Motion defaultStyle={{ opacity: 0 }} style={{ opacity: spring(1) }}>
        {(style: React.CSSProperties) =>
            (
                <div>
                    <div style={style} className="list-group-item">
                        <div className="row-action-primary">
                            <i className={`fa ${props.icon} fa-2x`}/>
                        </div>
                        <div className="row-content">
                            <div className="least-content">
                                <Link to={{ pathname: props.editLink, query: { id: props.id } }}>
                                    <button className="btn btn-lg btn-primary btn-raised">
                                        Edit
                            </button>
                                </Link>
                            </div>
                            <h4 className="list-group-item-heading">{props.name}</h4>
                            <p className="list-group-item-text">{props.id}</p>
                        </div>
                    </div>
                    <div className="list-group-separator"/>
                </div>
            )}
    </Motion>
);
