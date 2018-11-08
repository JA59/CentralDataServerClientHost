// ViewModel received from the AuthorizationController is response to a login request.
export interface TokenResponseVM {
    vm_token: string,
    vm_expiration: number,
    vm_username: string,
    vm_isadmin: boolean
}
