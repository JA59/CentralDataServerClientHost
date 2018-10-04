"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var DemoUser = /** @class */ (function () {
    function DemoUser(name, password) {
        this.Name = name;
        this.Password = password;
        this.Enabled = password.length > 0;
    }
    return DemoUser;
}());
exports.DemoUser = DemoUser;
//# sourceMappingURL=demouser.js.map