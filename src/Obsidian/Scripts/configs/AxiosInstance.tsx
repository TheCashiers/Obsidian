import * as Axios from "axios";
export let getAxios = (token: string) => {
    if (!token) {
        throw new TypeError("Token not found.");
    }
    return Axios.create({
        headers: { Authorization: `Bearer ${token}` },
    });
};
