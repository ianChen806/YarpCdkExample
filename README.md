# Welcome to your CDK C# project!

This is a blank project for CDK development with C#.

The `cdk.json` file tells the CDK Toolkit how to execute your app.

It uses the [.NET CLI](https://docs.microsoft.com/dotnet/articles/core/) to compile and execute your project.

## Useful commands

* `dotnet build src` compile this app
* `cdk deploy`       deploy this stack to your default AWS account/region
* `cdk diff`         compare deployed stack with current state
* `cdk synth`        emits the synthesized CloudFormation template

# 🛠️ AWS 練習專案 - TODO List

## 一、基礎設計

- [ ] 規劃 VPC 三層結構（public / app / db）
- [ ] 設定 Route Table、NAT Gateway、Internet Gateway
- [ ] 建立 IAM 角色與權限（ECS, S3, CloudWatch）

## 二、AWS 架構建設（建議使用 AWS CDK）

- [ ] 初始化 CDK 專案（建議使用 C# / TypeScript）
- [ ] 建立 VPC 結構
    - [ ] public / app / db subnet
    - [ ] 子網路的 Route Table 與路由規則
- [ ] 建立 IAM Role
    - [ ] ECS Task Role（存取 S3）
    - [ ] ECS Execution Role（啟動 Task 用）
- [ ] 建立 ECS Cluster（Fargate 模式）
- [ ] 建立 Application Load Balancer（ALB）與 Target Group
- [ ] 建立 S3 Bucket（存放 domain 對應表）
- [ ] 撰寫 CDK 程式碼並納入 Git 版控