# 🎓 AWS CDK 學習專案 - YARP 反向代理

## 專案目標
通過建立 YARP 反向代理專案，循序漸進地學習 AWS CDK 和核心 AWS 服務

## 📋 學習規則
- ✅ 一次只學習一個概念
- ✅ 每個步驟都有明確的學習目標
- ✅ 每個步驟完成後進行驗證
- ✅ 不跳過任何重要概念

## 🚀 學習計劃 (預計 10-12 週)

### 階段一：CDK 基礎 (1-2 週)
**學習目標**：掌握 CDK 基本概念和操作

#### 步驟 1: 驗證 CDK 環境
- [x] 確認 CDK 版本 (2.1020.2)
- [x] 確認 AWS CLI 配置
- [x] 設定預設 region (ap-northeast-2)
- [x] 更新學習計劃到 README.md
- [x] Git commit 學習進度 ✅ (commit: 9f0da17)
- [ ] 理解 CDK 專案結構
- [ ] 學習 CDK 基本指令

#### 步驟 2: 建立第一個簡單的 VPC
- [x] 學習 VPC 的基本概念
- [x] 用 CDK 建立最簡單的 VPC
- [x] 階段 2A: 理解專案結構 ✅
- [x] 階段 2B-1: 編譯專案成功 ✅
- [x] 階段 2B-2: 生成 CloudFormation 模板 (cdk synth) ✅
- [ ] 階段 2B-3: 查看變更 (cdk diff) ← 當前步驟
- [ ] 階段 2B-4: 部署到 AWS (cdk deploy)
- [ ] 階段 2C: 驗證部署結果
- [ ] 理解 CloudFormation 輸出

#### 步驟 3: 理解 CDK 的 Infrastructure as Code
- [ ] 學習 CDK 與 CloudFormation 的關係
- [ ] 理解 Stack 和 Construct 概念
- [ ] 練習修改和重新部署

### 階段二：網路基礎 (2-3 週)
**學習目標**：掌握 AWS 網路服務

#### 步驟 4: 建立 Subnet 和 Route Table
- [x] 學習 Public/Private Subnet 概念
- [x] 實作三層網路架構
- [ ] 理解路由表的配置
- [ ] 測試子網路連通性

#### 步驟 5: 網路閘道優化
- [x] 學習 Internet Gateway 概念
- [x] 理解 NAT Gateway 作用
- [ ] 移除 NAT Gateway 進行成本優化
- [ ] 設定 VPC Endpoints

#### 步驟 6: 實作 Security Groups
- [ ] 學習防火牆規則
- [ ] 設定安全群組
- [ ] 理解最小權限原則
- [ ] 測試安全群組規則

### 階段三：運算服務 (2-3 週)
**學習目標**：掌握容器化服務

#### 步驟 7: 建立 ECS Cluster
- [ ] 學習容器編排概念
- [ ] 建立 ECS Cluster (Fargate 模式)
- [ ] 理解 Fargate vs EC2 模式
- [ ] 學習 Fargate Spot 成本優化

#### 步驟 8: 建立第一個 ECS Service
- [ ] 學習 Task Definition 概念
- [ ] 部署簡單的 Hello World 容器
- [ ] 驗證容器運行狀態
- [ ] 理解 ECS Service 概念

#### 步驟 9: IAM 權限設定
- [ ] 建立 ECS Task Role
- [ ] 建立 ECS Execution Role
- [ ] 理解最小權限原則
- [ ] 測試權限設定

### 階段四：負載平衡器 (1-2 週)
**學習目標**：掌握流量分配

#### 步驟 10: 建立 Application Load Balancer
- [ ] 學習負載平衡器概念
- [ ] 配置 ALB 和 Target Group
- [ ] 設定健康檢查
- [ ] 測試負載平衡功能

#### 步驟 11: 網路整合測試
- [ ] 測試 ALB → ECS 連通性
- [ ] 驗證安全群組規則
- [ ] 測試故障轉移
- [ ] 理解網路流量路徑

### 階段五：YARP 應用 (2-3 週)
**學習目標**：實作具體應用

#### 步驟 12: 建立 YARP 專案
- [ ] 學習 YARP 基本概念
- [ ] 建立簡單的反向代理
- [ ] 學習 Docker 容器化
- [ ] 推送映像到 ECR

#### 步驟 13: S3 整合
- [ ] 學習 S3 基本操作
- [ ] 建立 S3 Bucket
- [ ] 實作 domain mapping 功能
- [ ] 測試動態配置更新

#### 步驟 14: ServiceB 後端服務
- [ ] 建立簡單的後端服務
- [ ] 配置 YARP 路由規則
- [ ] 測試端到端流量
- [ ] 驗證 header 注入功能

### 階段六：監控和維護 (1-2 週)
**學習目標**：掌握運維技能

#### 步驟 15: 設定 CloudWatch
- [ ] 學習監控概念
- [ ] 配置日誌群組
- [ ] 設定自定義指標
- [ ] 建立 CloudWatch Dashboard

#### 步驟 16: 成本最佳化
- [ ] 設定成本警示
- [ ] 分析成本分布
- [ ] 實施自動擴展
- [ ] 驗證成本節省效果

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

## 📊 成本預估

```
目標架構成本 (月費用)：
- ECS Fargate Spot: $15-25
- Application Load Balancer: $16
- S3 (domain mapping): $0-1
- CloudWatch Logs: $0-2
- 總計: $31-44/月
```

## 🎯 當前進度

**階段一：CDK 基礎**
- [x] 步驟 1: 環境驗證 ✅
- [ ] 步驟 2: 第一個 VPC (進行中)
- [ ] 步驟 3: Infrastructure as Code 概念

**目前狀態**: 
- ✅ CDK 版本確認 (2.1020.2)
- ✅ AWS CLI 配置完成
- ✅ Region 設定為 ap-northeast-2 (首爾)
- ✅ 學習計劃建立並提交到 Git (commit: 9f0da17)
- ✅ 專案編譯成功 (階段 2B-1)
- ✅ CloudFormation 模板生成成功 (階段 2B-2)
- 🔄 下一步：檢視變更清單 (cdk diff)

**下一個里程碑**: 完成 VPC 部署並理解 CDK 基本概念

## 🎉 恭喜！步驟 1 完全完成

您已經成功：
- ✅ 設定開發環境
- ✅ 配置 AWS CLI
- ✅ 建立學習計劃
- ✅ 學會基本的 Git 操作

**準備進入步驟 2**: 現在可以開始理解和部署您的第一個 VPC！

## 📍 Region 重要說明

**ap-northeast-2 (首爾) 的優勢**:
- 低延遲 (適合亞洲地區)
- 完整的 AWS 服務支援
- 相對較低的成本

**成本預估調整 (ap-northeast-2)**:
```
目標架構成本 (月費用，首爾地區)：
- ECS Fargate Spot: $16-28
- Application Load Balancer: $18
- S3 (domain mapping): $0-1
- CloudWatch Logs: $0-2
- 總計: $34-49/月
```

## 🚀 下一步操作指南 - 步驟 2: VPC 部署

**💡 環境變數疑問解答**

**Q: 沒有設定 CDK_DEFAULT_ACCOUNT 和 CDK_DEFAULT_REGION 會有問題嗎？**
**A: 不會！** 對於學習階段完全沒問題：

✅ **目前階段（學習用）**：
- `cdk synth` 會正常工作
- `cdk deploy` 會使用您的 AWS CLI 預設設定
- 模板會是 environment-agnostic（通用型）

🔧 **將來可能需要設定的情況**：
- 當您需要查詢特定 region 的 AZ 數量
- 當您需要多環境部署（dev/staging/prod）
- 當您的程式碼需要 region/account 特定的功能

**階段 2A: 理解 CDK 專案結構**

首先，讓我們了解您的專案結構：
```
AwsCdkStack/
├── src/AwsCdkStack/
│   ├── Program.cs           ← CDK 應用程式進入點
│   ├── AwsCdkStackStack.cs  ← 您的 VPC 程式碼在這裡
│   └── AwsCdkStack.csproj   ← 專案相依性
├── cdk.json                 ← CDK 配置檔
└── README.md               ← 學習進度追蹤
```

**階段 2B: CDK 基本指令實作**

1. **編譯專案** (確保程式碼正確):
   ```bash
   cd src
   dotnet build
   ```
   📝 **學習重點**: C# 編譯過程，檢查語法錯誤

2. **🔄 生成 CloudFormation 模板** (理解 Infrastructure as Code) - **下一步**:

   **⚠️ 重要概念：環境變數和 CDK 模式**
   
   您的 Program.cs 使用了環境變數：
   ```csharp
   Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
   Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
   ```
   
   **如果沒有設定環境變數會怎樣？**
   - ✅ `cdk synth` **仍然會工作**
   - ✅ CDK 會使用 **environment-agnostic** 模式
   - ✅ 生成的模板可以部署到任何 region/account
   - ⚠️ 但某些功能會受限（如 AZ 查詢、AMI 查詢等）
   
   **cdk synth 的作用**:
   ```bash
   cdk synth
   ```
   📝 **這個指令做什麼**:
   - 🔄 將您的 C# CDK 程式碼轉換成 CloudFormation JSON
   - 🔍 驗證程式碼語法和邏輯
   - 📄 生成 AWS 實際執行的基礎設施定義
   - 💾 輸出到 `cdk.out/` 資料夾
   
   📝 **學習重點**: 理解 Infrastructure as Code 的轉換過程
   💡 **觀察要點**: 
   - 輸出的 JSON 結構
   - VPC 相關的資源定義
   - CDK 自動生成的資源名稱
   - 注意任何警告訊息

3. **查看將要建立的資源** (部署前檢查):
   ```bash
   cdk diff
   ```
   📝 **學習重點**: 基礎設施變更追蹤和版本控制

4. **部署到 AWS** (實際建立資源):
   ```bash
   cdk deploy
   ```
   📝 **學習重點**: CloudFormation Stack 建立過程

**階段 2C: 驗證和學習**

5. **AWS Console 驗證**:
   - 🌐 登入 [AWS Console](https://ap-northeast-2.console.aws.amazon.com/vpc/)
   - 📍 確認在 ap-northeast-2 region
   - 🔍 查看 VPC 服務 → 檢查您的 VPC
   - 📊 查看 CloudFormation → 檢查 Stack 狀態

6. **記錄學習成果**:
   - VPC ID 和 CIDR Block
   - 建立了哪些子網路
   - Route Table 配置
   - 任何觀察到的有趣現象

**⚠️ 重要學習提醒**:
- 📚 每個指令執行後，觀察輸出並提問
- 💰 注意成本：VPC 本身免費，但要留意 NAT Gateway
- 🔒 確保有適當的 IAM 權限
- ⏱️ 首次部署可能需要 5-10 分鐘
- 📝 隨時在 README.md 中記錄問題和發現

---

## 📚 學習資源

- [AWS CDK Developer Guide](https://docs.aws.amazon.com/cdk/v2/guide/)
- [AWS Well-Architected Framework](https://aws.amazon.com/architecture/well-architected/)
- [YARP Documentation](https://microsoft.github.io/reverse-proxy/)