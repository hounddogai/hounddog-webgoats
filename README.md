# HoundDog.ai Webgoats

## Overview

This repository contains sample application codebases with deliberate security vulnerabilities (known as
[webgoats](https://owasp.org/www-project-webgoat/)) for demonstrating the capabilities of the
[HoundDog.ai](https://hounddog.ai/) scanner. The following languages are currently covered:
- Java
- C# / .NET
- SQL
- GraphQL
- OpenAPI / Swagger

## Try it Out!
To try out the HoundDog.ai scanner, first clone this repository:
```bash
git clone https://github.com/hounddogai/hounddog-webgoats.git
```
Then run the scanner Docker image to generate a markdown report:
```bash
docker run -t -v ./hounddog-webgoats:/scanpath hounddogai/scanner-free hounddog scan --output-format=markdown --output-filename=report.md
```
For viewing the `report.md` file, we recommend the [Markdown Viewer](https://chromewebstore.google.com/detail/markdown-viewer/ckkdlimhmcjmikdlpkmbgfkaikojcbjk) 
browser extension with **mermaid** and **toc** content options enabled. For more information, 
please refer to our [user documentation](https://docs.hounddog.ai/scanner/markdown-report).

## CI/CD Integration

This repository also includes sample YAML configuration files for enabling the scanner in various CI tools such as
Azure Pipelines, BitBucket Pipelines, Circle CI, GitHub Actions, GitLab CI/CD and Jenkins Pipeline. 

## Contact Us

If you need any help or would like to send us feedback, please shoot us an email at support@hounddog.ai.
