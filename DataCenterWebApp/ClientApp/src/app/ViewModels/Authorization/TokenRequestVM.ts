// ViewModel passed to the AuthorizationController when logging in.
export interface TokenRequestVM {
  vm_grant_type: string,
  vm_client_id: string,
  vm_client_secret: string,
  vm_username: string,
  vm_password: string
}
