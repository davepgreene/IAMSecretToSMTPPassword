## Synopsis

A WPF C# app designed to take an AWS IAM secret key and generate an AWS SES SMTP password.

## Motivation

Amazon Web Services SES (Simple Email Service) requires its own specially generated SMTP password that is derived from the IAM secret key.

See [Obtaining Your Amazon SES SMTP Credentials](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/smtp-credentials.html) in the AWS documentation.

While Amazon doesn't recommend this practice it can be unwieldy to manage two sets of credentials for every IAM user. It's simply easier to generate a second key with only "ses:SendRawEmail" permissions.

## Installation

Build using the Release profile.

## License

Distributed under the Simplified BSD license. See [LICENSE.md](LICENSE.md) for full license text.