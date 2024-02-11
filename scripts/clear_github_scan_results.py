import requests
from environs import Env

env = Env()
github_token = env.str("GITHUB_TOKEN")


def clear_github_scan_results(repo_name):
    headers = {
        "Authorization": f"Bearer {github_token}",
        "X-GitHub-Api-Version": "2022-11-28",
    }
    base_url = "https://api.github.com/repos/hounddogai"

    while True:
        response = requests.get(
            url=f"{base_url}/{repo_name}/code-scanning/analyses",
            headers=headers,
        )
        if not str(response.status_code).startswith("2"):
            print(response.content, response.status_code)
            break

        analyses = response.json()
        print(f"Found {len(analyses)} analyses")

        for analysis in analyses:
            response = requests.delete(
                f"{base_url}/{repo_name}/code-scanning/analyses/{analysis['id']}?confirm_delete",
                headers=headers,
            )
            if str(response.status_code).startswith("2"):
                print(f"Deleted code scanning analysis {analysis['id']}")
            else:
                print(response.content, response.status_code)


if __name__ == "__main__":
    clear_github_scan_results("hounddog-webgoats")
