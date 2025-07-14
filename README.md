# 🎓 AWS CDK 學習專案 - YARP 反向代理

## 📋 專案概述
**目標**: 通過建立 YARP 反向代理專案，循序漸進地學習 AWS CDK 和核心 AWS 服務
**學習方式**: 一次只學習一個概念，每個步驟都有明確的學習目標

## 🎯 當前進度狀態
- ✅ **階段一**：CDK 基礎 - 完成
- ✅ **階段二**：網路基礎 - 完成  
- ✅ **階段三**：運算服務 - 完成
- ✅ **階段四**：負載平衡器 - 完成
- 🔄 **階段五**：YARP 應用 - 進行中 (步驟15: S3配置管理整合)

## 🚀 完整學習計劃 (10-12 週)

### 階段一：CDK 基礎 (1-2 週) ✅
**學習目標**：掌握 CDK 基本概念和操作

- [x] **步驟 1**: 驗證 CDK 環境
  - CDK 版本 (2.1020.2)、AWS CLI 配置、預設 region (ap-northeast-2)
- [x] **步驟 2**: 建立第一個簡單的 VPC
  - VPC 基本概念、CDK 專案結構、基本指令、CloudFormation 模板
- [x] **步驟 3**: 理解 CDK 的 Infrastructure as Code
  - CDK 與 CloudFormation 關係、Stack 和 Construct 概念、成本影響分析

### 階段二：網路基礎 (2-3 週) ✅
**學習目標**：掌握 AWS 網路服務

- [x] **步驟 4**: 建立 Subnet 和 Route Table
  - Public/Private Subnet 概念、三層網路架構、路由表配置
- [x] **步驟 5**: 網路閘道優化
  - Internet Gateway、NAT Gateway、VPC Endpoints 概念和成本優化
- [x] **步驟 6**: 實作 Security Groups
  - 防火牆規則、最小權限原則、Security Group 引用機制

### 階段三：運算服務 (2-3 週) ✅
**學習目標**：掌握容器化服務

- [x] **步驟 7**: 建立 ECS Cluster
  - 容器編排概念、ECS Cluster (Fargate 模式)、Fargate vs EC2 模式
- [x] **步驟 8**: 建立第一個 ECS Service
  - Task Definition、ECS Service、網路架構和映像拉取
- [x] **步驟 9**: IAM 權限設定
  - ECS Task Role、ECS Execution Role、ServicePrincipal 概念、最小權限原則

### 階段四：負載平衡器 (1-2 週) ✅
**學習目標**：掌握流量分配

- [x] **步驟 10**: 建立 Application Load Balancer
  - 負載平衡器概念、ALB 和 Target Group 配置、健康檢查
- [x] **步驟 11**: 網路整合測試
  - ALB → ECS 連通性、Security Groups 規則驗證

### 階段五：YARP 應用 (2-3 週) 🔄
**學習目標**：實作具體應用

- [x] **步驟 12**: 建立 YARP 專案
  - YARP 基本概念、反向代理功能、Docker 容器化、ECR 映像推送
- [x] **步驟 12A**: ECS 部署更新
  - ECR 映像整合、雙容器架構、容器間通訊、CDK Context 安全實踐
- [x] **步驟 13**: 架構分離優化
  - Multi-Stack 架構設計、InfrastructureStack vs ApplicationStack
  - Cross-Stack References 學習、現代CDK vs 傳統CloudFormation方式
- [x] **步驟 14**: ServiceB 後端服務
  - 完整後端 API、YARP 路由配置、Header Transform 功能
- [ ] **步驟 15**: S3 配置管理整合 🗂️ ← **當前進度**
  - 企業級動態配置管理、S3 SDK 整合、記憶體快取、IAM 權限

### 階段六：監控和維護 (1-2 週)
**學習目標**：掌握運維技能

- [ ] **步驟 16**: 設定 CloudWatch
  - 監控概念、日誌群組、自定義指標、Dashboard
- [ ] **步驟 17**: 成本最佳化
  - 成本警示、成本分析、自動擴展

## 🏗️ 當前架構概覽

### 網路架構
```
VPC (10.0.0.0/16)
├── Public Subnet (10.0.1.0/24, 10.0.2.0/24)    # ALB
├── App Subnet (10.0.11.0/24, 10.0.12.0/24)     # ECS
└── Database Subnet (10.0.21.0/24, 10.0.22.0/24) # RDS (未來)
```

### 應用架構
```
Internet → ALB → ECS Service → [YarpProxy + YarpTarget] → S3 Config
```

### Multi-Stack 分離
- **InfrastructureStack**: VPC、Subnets、Route Tables、S3 Gateway Endpoint
- **ApplicationStack**: Security Groups、ALB、ECS、IAM Roles、CloudWatch

## 💰 成本預估 (ap-northeast-2)
```
目標架構成本 (月費用，首爾地區)：
- ECS Fargate Spot: $16-28
- Application Load Balancer: $18
- S3 (domain mapping): $0-1
- CloudWatch Logs: $0-2
- 總計: $34-49/月
```

## 🛠️ 常用 CDK 指令
```bash
# 編譯專案
dotnet build src

# 查看變更
cdk diff

# 部署到 AWS
cdk deploy

# 生成 CloudFormation 模板
cdk synth

# 清理資源
cdk destroy
```

## 🎯 重要學習成果

### 階段一成果
- ✅ CDK 基本操作和 Infrastructure as Code 概念
- ✅ CloudFormation 模板生成和部署流程
- ✅ AWS 環境配置和基本 VPC 建立

### 階段二成果
- ✅ 完整 AWS 網路架構：三層網路設計
- ✅ 安全設計：Security Groups 最小權限原則
- ✅ 成本優化：Gateway Endpoints vs Interface Endpoints 權衡

### 階段三成果
- ✅ 容器編排：ECS Cluster 和 Service 管理
- ✅ IAM 權限：Task Role 和 Execution Role 分離
- ✅ 故障排除：CloudWatch Logs 診斷技能

### 階段四成果
- ✅ 負載平衡：ALB 和 Target Group 配置
- ✅ 網路整合：完整的 Internet → ALB → ECS 流量路徑
- ✅ 高可用性：跨 AZ 部署和健康檢查

### 階段五成果
- ✅ 容器化應用：YARP 反向代理完整實作
- ✅ 雲端部署：ECR 映像管理和 ECS 部署
- ✅ 架構進階：Multi-Stack 設計和 Cross-Stack 整合
- ✅ 企業級實踐：動態配置管理和錯誤處理

## 📚 學習資源
- [AWS CDK Developer Guide](https://docs.aws.amazon.com/cdk/v2/guide/)
- [AWS Well-Architected Framework](https://aws.amazon.com/architecture/well-architected/)
- [YARP Documentation](https://microsoft.github.io/reverse-proxy/)

## 🔄 下一步：步驟 15 - S3 配置管理整合

**學習目標**: 實作企業級動態配置管理，從 S3 取得 domain 清單並快取

**技術架構演進**：
```
當前：DomainHeaderService (靜態 Dictionary)
新架構：S3 Bucket → S3ConfigurationService → IMemoryCache → DomainHeaderService
```

**實作階段**：
- A. 理解現有架構和設計整合點
- B. S3 服務設計和 AWS SDK 整合
- C. 記憶體快取和錯誤處理
- D. CDK 基礎設施更新 (S3 權限和 Bucket)
- E. 測試和驗證

**預期學習收穫**：
- 🔧 AWS S3 SDK 在 .NET 中的使用
- 🗂️ 企業級配置管理最佳實踐
- 🧠 IMemoryCache 和錯誤處理策略
- 🛡️ IAM 最小權限原則的 S3 實作
- 🏗️ Infrastructure as Code 的進階整合