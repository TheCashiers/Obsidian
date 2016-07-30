import { Link } from "react-router";

const UserItem = (props) => (
    <li>
        {props.username}
    </li>
);

export const UserList = (props) => (
    <div className="content-wrapper">
        <h1>Users: </h1>
        <ul>
            {props.users.map((user, index) => <UserItem username={user.userName} key={user.id}/>) }
        </ul>
        <hr/>
        <Link to="/manage/users/create">
            <button className="btn btn-lg btn-success">Create User</button>
        </Link>
    </div>
);