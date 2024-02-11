export const environment = {
  production: true,
  apiUrl: 'https://virtual-sys.herokuapp.com'
};

export function getApiUrl():string {
  if(window && window.location){
    return window.location.origin.replace("4200","8080");
  }
  return this.apiUrl;
}
