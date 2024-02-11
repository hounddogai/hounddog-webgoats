import os

import requests

GITLAB_TOKEN = os.environ["GITLAB_TOKEN"]
GITLAB_API_URL = "https://gitlab.com/api/v4"
GITLAB_PROJECT_ID = 54824810
REQUEST_HEADERS = {"Private-Token": GITLAB_TOKEN}


def list_vulnerabilities():
    response = requests.get(
        f"{GITLAB_API_URL}/projects/{GITLAB_PROJECT_ID}/vulnerabilities",
        headers=REQUEST_HEADERS,
    )
    if response.status_code != 200:
        raise Exception(
            f"Failed to list vulnerabilities: {response.text} ({response.status_code})"
        )
    return response.json()


def resolve_vulnerability(vulnerability_id):
    response = requests.post(
        f"{GITLAB_API_URL}/vulnerabilities/{vulnerability_id}/resolve",
        headers=REQUEST_HEADERS,
    )
    if response.status_code not in (201, 304):
        raise Exception(
            f"Failed to resolve vulnerability {vulnerability_id}: {response.text} ({response.status_code})"
        )


if __name__ == "__main__":
    for vulnerability in list_vulnerabilities():
        if vulnerability["state"] == "resolved":
            continue
        print(f"Resolving vulnerability {vulnerability['id']}")
        resolve_vulnerability(vulnerability["id"])
