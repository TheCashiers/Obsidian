import { Link } from "react-router";

const UserItem = (props) => (
    <li>
        {props.username}
    </li>
);

export const UserList = (props) => (
    <div className="content-wrapper content">
        <div className="box box-solid box-info">
            <div className="box-header">
                <h3 className="box-title">Users</h3>
            </div>
            <div className="box-body">
            <ul>{props.users.map((user, index) => <UserItem username={user.userName} key={user.id}/>) }</ul>
            </div>
        </div>
        <Link to="/manage/users/create">
            <button className="btn btn-lg btn-success">Create User</button>
        </Link>
    </div>

);