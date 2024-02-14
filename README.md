# HoundDog.ai Webgoats

![GitHub language count](https://img.shields.io/github/languages/count/hounddogai/hounddog-webgoats?style=flat)
[![Azure DevOps builds](https://img.shields.io/azure-devops/build/hounddogai/7b0bba81-2e11-4557-b4f2-e225fb4910af/2?style=flat&label=azure)](https://dev.azure.com/hounddogai/hounddog-webgoats/_build?view=runs)
[![Bitbucket Pipelines](https://img.shields.io/bitbucket/pipelines/hounddogai-ws/hounddog-webgoats/main?label=bitbucket)](https://bitbucket.org/hounddogai-ws/hounddog-webgoats/pipelines)
[![CircleCI](https://dl.circleci.com/status-badge/img/circleci/NXw6bAeqRWGf6eSe4tD2e4/DH8v1HFnchC569tyasUPqA/tree/main.svg?style=shield&circle-token=fd6d86559b76ba9839e70f00fe067573e112f701)](https://dl.circleci.com/status-badge/redirect/circleci/NXw6bAeqRWGf6eSe4tD2e4/DH8v1HFnchC569tyasUPqA/tree/main)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/hounddogai/hounddog-webgoats/hounddog.yml?label=github)](https://github.com/hounddogai/hounddog-webgoats/actions/workflows/hounddog.yml)
[![Gitlab Pipeline Status](https://img.shields.io/gitlab/pipeline-status/hounddogai%2Fhounddog-webgoats?style=flat&label=gitlab)](https://gitlab.com/hounddogai/hounddog-webgoats/-/pipelines)
[![Jenkins Build](https://img.shields.io/jenkins/build?jobUrl=https%3A%2F%2Fjenkins.hounddog.ai%2Fjob%2Fhounddog-webgoats%2F&style=flat&label=jenkins)](https://jenkins.hounddog.ai/job/hounddog-webgoats/)

## Overview

This repository contains a collection of sample application codebases with deliberate security vulnerabilities (known
as [webgoats](https://owasp.org/www-project-webgoat/)) for demonstrating the capabilities of the
[HoundDog.ai](https://hounddog.ai/) scanner. The following languages are currently covered:

- Java
- C# / .NET
- SQL
- GraphQL
- OpenAPI / Swagger

The READMEs for each webgoat application can be found in
their [respective directories](https://github.com/hounddogai/hounddog-webgoats/tree/main/webgoats).

## Try it Out!

To try out the HoundDog.ai scanner, first clone this repository:

```bash
git clone https://github.com/hounddogai/hounddog-webgoats.git
```

Then run the scanner Docker image to generate a markdown report:

```bash
docker run -t -v ./hounddog-webgoats:/scanpath hounddogai/scanner-free hounddog scan --output-format=markdown --output-filename=report.md
```

For viewing the `report.md` file, we recommend
the [Markdown Viewer](https://chromewebstore.google.com/detail/markdown-viewer/ckkdlimhmcjmikdlpkmbgfkaikojcbjk)
browser extension with **mermaid** and **toc** content options enabled. For more information,
please refer to our [user documentation](https://docs.hounddog.ai/scanner/markdown-report).

## CI/CD Integration

This repository also includes sample YAML configuration files for enabling the scanner in various CI tools such as
Azure Pipelines, BitBucket Pipelines, Circle CI, GitHub Actions, GitLab CI/CD and Jenkins Pipeline.

## Contact Us

If you need any help or would like to send us feedback, please shoot us an email at support@hounddog.ai.
