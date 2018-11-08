"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// Object provided by AuthService subscription
var AuthData = /** @class */ (function () {
    function AuthData(userToken, loggedOnUser, displayName, isAdmin) {
        this.userToken = userToken;
        this.loggedOnUser = loggedOnUser;
        this.displayName = displayName;
        this.isAdmin = isAdmin;
    }
    AuthData.prototype.isLoggedIn = function () {
        return (this.loggedOnUser && this.userToken) ? true : false;
    };
    AuthData.prototype.isLoggedInAsAdmin = function () {
        return (this.isLoggedIn() && this.isAdmin);
    };
    AuthData.prototype.isLoggedInAsUser = function () {
        return (this.isLoggedIn() && !this.isAdmin);
    };
    AuthData.prototype.isLoggedInAsGuest = function () {
        return (!this.isLoggedIn());
    };
    return AuthData;
}());
exports.AuthData = AuthData;
//# sourceMappingURL=AuthData.js.map