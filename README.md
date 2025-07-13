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
- [x] 階段 2B-3: 查看變更 (cdk diff) ✅
- [x] 階段 2B-3.1: 成本優化修改 (移除 NAT Gateway) ✅
- [x] 階段 2B-4a: CDK Bootstrap (環境初始化) ✅
- [x] 階段 2B-4b: 部署到 AWS (cdk deploy) ✅
- [x] 階段 2C: 驗證部署結果 ✅
- [x] 理解 CloudFormation 輸出 ✅

#### 🎉 步驟 2 完全完成！

#### 步驟 3: 理解 CDK 的 Infrastructure as Code
- [x] 學習 CDK 與 CloudFormation 的關係 ✅
- [x] 理解 Stack 和 Construct 概念 ✅
- [x] 練習修改和重新部署 ✅
- [x] 體驗 Infrastructure as Code 的威力 ✅
- [x] 理解成本影響分析 ✅

**🎉 步驟 3 完全完成！** 

**重要學習成果**：
- ✅ 一行程式碼修改觸發複雜基礎設施變化
- ✅ CDK 智慧推論：PRIVATE_WITH_EGRESS → NAT Gateway + EIP
- ✅ 成本意識：NAT Gateway 月費 ~$75，選擇成本優化
- ✅ 最佳實務：自動跨 AZ 部署確保高可用性
- ✅ 學習策略：在學習階段控制成本，保持 PRIVATE_ISOLATED

### 階段二：網路基礎 (2-3 週)
**學習目標**：掌握 AWS 網路服務

#### 步驟 4: 建立 Subnet 和 Route Table
- [x] 學習 Public/Private Subnet 概念 ✅
- [x] 實作三層網路架構 ✅
- [x] 理解路由表的配置 ✅
- [ ] 測試子網路連通性 ← 下一步

**🎉 步驟 4A 完成！**

**重要理解成果**：
- ✅ 路由表數量：7個 (6個CDK + 1個Default)
- ✅ Public vs Private 差異：0.0.0.0/0 路由的存在
- ✅ 流量路徑：Public (local→IGW→外部) vs Private (timeout)
- ✅ 網路隔離實現：透過路由表控制而非物理隔離

**🎉 步驟 4B: 架構設計思維 完成！**

**進階架構理解**：
- ✅ 三層架構設計：ALB(Public) → ECS(App) → RDS(Database)
- ✅ 高可用性：跨 AZ 部署避免單點故障
- ✅ 安全性分層：透過 Subnet 隔離 + Security Groups 控制
- ✅ 成本最佳化：VPC Endpoints vs NAT Gateway 權衡分析
- ✅ 合規性考量：資料庫完全隔離，便於審計和監控

#### 步驟 5: 網路閘道優化
- [x] 學習 Internet Gateway 概念 ✅
- [x] 理解 NAT Gateway 作用 ✅
- [x] 學習 VPC Endpoints 概念 ✅
- [x] 實作 VPC Endpoints 進行成本優化 ✅

**🎉 步驟 5C: 實作 S3 Gateway Endpoint 完成！**

**完成成果**：
- ✅ 在 CDK 中新增 S3 Gateway Endpoint 程式碼
- ✅ 執行 `cdk diff` 觀察基礎設施變化
- ✅ 分析路由表的自動更新機制
- ✅ 理解免費 Gateway Endpoint 的價值
- ✅ 掌握 CDK 子網路選擇邏輯 (`SubnetGroupName`)
- ✅ 驗證 App 和 Database 子網路都正確關聯

**重要學習成果**：
- CDK 子網路選擇：`SubnetType` vs `SubnetGroupName`
- AWS 服務代管：自動路由表更新機制
- Infrastructure as Code：精確控制 vs 智慧推論
- 網路架構實務：私有子網路的 S3 存取優化

**🎉 步驟 5A: VPC Endpoints 理論完成！**

**重要概念掌握**：
- ✅ Gateway Endpoints：免費，支援 S3/DynamoDB，保持私有 + 節省成本
- ✅ Interface Endpoints：付費($0.01/小時)，支援大多數 AWS 服務
- ✅ 成本比較：Interface Endpoints ($14.4/月) vs NAT Gateway ($34/月)
- ✅ 架構影響：直接私有連接 vs 透過 Internet 路由

**🎉 步驟 5B: 成本效益分析完成！**

**深度理解**：
- ✅ 適用場景：2-5個 AWS 服務時 Interface Endpoints 更划算
- ✅ 必須使用 NAT Gateway：第三方服務、軟體包下載、固定IP需求
- ✅ 混合策略：開發階段彈性，生產階段成本優化
- ✅ YARP 專案建議：Gateway(S3) + Interface(ECR+CloudWatch) ≈ $21.6/月

#### 步驟 6: 實作 Security Groups
- [x] 學習防火牆規則 ✅
- [x] 設定安全群組 ✅
- [x] 理解最小權限原則 ✅
- [x] 測試安全群組規則 ✅

**🎉 步驟 6 完全完成！**

**實作成果**：
- ✅ 建立 ALB Security Group (HTTP/HTTPS from Internet)
- ✅ 建立 ECS Security Group (TCP/8080 from ALB only)
- ✅ 實現 Security Group 引用機制
- ✅ 應用最小權限原則
- ✅ 成功部署並驗證設定

**重要學習成果**：
- 🛡️ **防火牆規則設計**：Ingress/Egress 的精確控制
- 🔗 **Security Group 引用**：動態安全控制的威力
- 🎯 **最小權限原則**：只開放必要的存取
- 🚀 **Infrastructure as Code**：程式化的安全管理

### 階段三：運算服務 (2-3 週)
**學習目標**：掌握容器化服務

#### 步驟 7: 建立 ECS Cluster
- [x] 學習容器編排概念 ✅
- [x] 建立 ECS Cluster (Fargate 模式) ✅
- [x] 理解 Fargate vs EC2 模式 ✅
- [x] 驗證 Cluster 部署成功 ✅

**🎉 步驟 7 完全完成！**

**重要學習成果**：
- ✅ ECS Cluster 是**邏輯容器**，本身免費
- ✅ CDK 命名規則：自動加入 Stack 名稱和唯一 ID
- ✅ Fargate 模式：serverless 容器運行環境
- ✅ Infrastructure as Code：漸進式建構複雜架構

#### 步驟 8: 建立第一個 ECS Service
- [x] 學習 Task Definition 概念 ✅
- [x] 部署簡單的 Hello World 容器 ✅
- [x] 驗證容器運行狀態 ✅
- [x] 理解 ECS Service 概念 ✅
- [x] 解決 Fargate Port 配置問題 ✅
- [x] 修正網路架構（PRIVATE_WITH_EGRESS）✅
- [x] 處理映像拉取網路依賴 ✅

**🎉 步驟 8 完全完成！**

**重要學習成果**：
- ✅ **Task Definition vs Service**：藍圖 vs 生命週期管理
- ✅ **Fargate 網路限制**：awsvpc 模式的 Port 配置
- ✅ **映像拉取機制**：網路架構對容器的實際影響
- ✅ **CloudFormation 依賴**：資源建立順序和重試機制
- ✅ **成本優化思維**：MaxAzs=1 節省 50% NAT Gateway 費用
- ✅ **錯誤診斷技能**：從 Task 失敗到根本原因分析

#### 步驟 9: IAM 權限設定
- [x] 建立 ECS Task Role ✅
- [x] 建立 ECS Execution Role ✅
- [x] 理解最小權限原則 ✅
- [x] 測試權限設定 ✅
- [x] 學習 ServicePrincipal 概念 ✅
- [x] 掌握 CDK 依賴管理 ✅
- [x] 處理容器健康檢查配置 ✅

**🎉 步驟 9 完全完成！**

**重要學習成果**：
- ✅ **IAM 雙重角色架構**：Task Role（應用程式權限）vs Execution Role（ECS 服務權限）
- ✅ **ServicePrincipal 信任關係**：`ecs-tasks.amazonaws.com` 的作用和來源
- ✅ **最小權限原則實踐**：精確的 S3 存取控制 (`arn:aws:s3:::bucket/*`)
- ✅ **CDK 依賴管理最佳實務**：避免過度的顯式依賴
- ✅ **容器映像工具依賴**：nginx 映像的極簡特性
- ✅ **錯誤處理和快速修復**：CloudFormation 回滾機制

### 階段四：負載平衡器 (1-2 週)
**學習目標**：掌握流量分配

#### 步驟 10: 建立 Application Load Balancer
- [x] 學習負載平衡器概念 ✅
- [x] 配置 ALB 和 Target Group ✅
- [x] 設定健康檢查 ✅
- [x] 測試負載平衡功能 ✅

**🎉 步驟 10 完全完成！**

**本次學習重點與驗證成果**：
- ✅ ALB 必須跨兩個 AZ 部署，否則 CloudFormation 會報錯（"At least two subnets in two different Availability Zones must be specified"）
- ✅ 成功調整 VPC 設定，讓 Public Subnet 跨兩個 AZ
- ✅ ALB 狀態 active，Target Group 健康檢查為 healthy
- ✅ ALB DNS 可正常顯示 nginx 歡迎頁面，流量順利導向 ECS Task
- ✅ 熟悉 ALB、Target Group、Listener、Security Group 的正確設計與關聯

#### 步驟 11: 網路整合測試
- [x] 測試 ALB → ECS 連通性 ✅
- [x] 驗證安全群組規則 ✅
- [ ] 測試故障轉移
- [ ] 理解網路流量路徑

**🎉 步驟 11A: 網路整合測試 進行中！**

**實作成果**：
- ✅ ALB → ECS 連通性測試完成，流量正常導向 nginx 容器
- ✅ Security Groups 規則驗證通過：ALB SG 允許 Internet 存取，ECS SG 僅允許來自 ALB 的流量
- ✅ 建立 YarpProxy 專案：包含 Header Transform 和 Domain Header Service 功能
- ✅ 建立 YarpTarget 專案：WeatherForecast API 作為後端服務
- ✅ 配置 YARP 反向代理：YarpProxy → YarpTarget (localhost:5128) 路由設定完成
- ✅ 本機測試完成：YarpTarget 服務正常運行於 localhost:5128
- ✅ 反向代理功能驗證：YarpProxy 成功轉發請求到後端服務
- ✅ Header Transform 功能確認：X-Source → Source header 對應正常運作
- ✅ API 端點測試：/source 端點成功回傳自定義 header 資訊

### 階段五：YARP 應用 (2-3 週)
**學習目標**：實作具體應用

#### 步驟 12: 建立 YARP 專案
- [x] 學習 YARP 基本概念 ✅
- [x] 建立簡單的反向代理 ✅
- [x] 建立後端測試服務 ✅
- [x] 本機測試 YARP 反向代理功能 ✅
- [x] 修改 YARP 配置適應容器環境 ✅
- [x] 學習 Docker 容器化 ✅
- [x] 建立 YarpProxy Dockerfile ✅
- [x] 建立 YarpTarget Dockerfile ✅
- [x] 修正 Port 配置一致性 ✅
- [x] 本機測試 Docker 容器 ✅
  - [x] YarpTarget 容器建構和測試成功 ✅
  - [x] YarpProxy 容器建構和測試 ✅
  - [x] 容器間通訊測試成功 ✅
  - [x] Docker Compose 整合測試成功 ✅
- [x] 推送映像到 ECR ✅
  - [x] 建立 yarp-proxy ECR 儲存庫 ✅
  - [x] 建立 yarp-target ECR 儲存庫 ✅
  - [x] ECR 登入和身份驗證 ✅
  - [x] 映像重新標記 (Tag) ✅
  - [x] 推送映像到 ECR ✅
  - [x] 驗證映像推送成功 ✅

**🎉 步驟 12 完全完成！**

**重要學習成果**：
- ✅ **Docker 容器化**：掌握多階段構建和映像優化
- ✅ **Docker Compose**：成功模擬 ECS Task Definition 環境
- ✅ **容器間通訊**：解決網路命名空間和服務發現問題
- ✅ **ECR 整合**：完成映像推送和雲端儲存庫管理
- ✅ **多平台映像**：理解 Image Index 和平台架構概念
- ✅ **成本控制**：在免費額度內完成雲端部署準備

**技術架構里程碑**：
- 🐳 本機容器化開發環境完成
- ☁️ 雲端映像儲存庫部署完成
- 🔄 反向代理應用程式容器化完成
- 🎯 準備進入 ECS 部署階段

#### 步驟 12A: ECS 部署更新（優先處理）
- [x] 更新 CDK Task Definition 使用 ECR 映像 ✅
- [x] 修改容器定義支援 YARP 雙容器架構 ✅
- [x] 配置容器間通訊 (localhost) ✅
- [x] 更新 Port 和健康檢查設定 ✅
- [ ] 配置 CDK Context 映像 URI ← **當前任務**
- [ ] 部署新的 Task Definition 到 ECS
- [ ] 驗證 ALB → ECS → YARP 完整鏈路
- [ ] 測試反向代理功能

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

**階段一：CDK 基礎** ✅ **完成**
- [x] 步驟 1: 環境驗證 ✅
- [x] 步驟 2: 第一個 VPC ✅  
- [x] 步驟 3: Infrastructure as Code 概念 ✅

**階段二：網路基礎** ✅ **完成**
- [x] 步驟 4: 建立 Subnet 和 Route Table ✅
- [x] 步驟 5: 網路閘道優化 ✅
- [x] 步驟 6: 實作 Security Groups ✅

**階段三：運算服務** ✅ **完成**
- [x] 步驟 7: 建立 ECS Cluster ✅
- [x] 步驟 8: 建立第一個 ECS Service ✅
- [x] 步驟 9: IAM 權限設定 ✅

**階段四：負載平衡器** 🔄 **進行中**
- [x] 步驟 10: 建立 Application Load Balancer ✅
- [x] 步驟 11: 網路整合測試 (進行中) ✅

**階段五：YARP 應用** 🔄 **開始**
- [x] 步驟 12: 建立 YARP 專案 (進行中) ✅

**目前狀態**: 
- ✅ ALB → ECS 架構完成並通過連通性和安全測試
- ✅ 建立 YarpProxy 和 YarpTarget 專案，配置完成
- 🔄 下一步：本機測試 YARP 反向代理功能

**🎉 重要里程碑達成**: 階段二：網路基礎 完全完成！成功建立完整的 AWS 網路架構，準備進入運算服務學習！

## 🎉 恭喜！步驟 5：網路閘道優化 完全完成！

您已經成功：
- ✅ 完全理解 VPC Endpoints 的價值和原理
- ✅ 深入分析成本效益權衡
- ✅ 實作 S3 Gateway Endpoint
- ✅ 掌握 CDK 子網路選擇邏輯
- ✅ 驗證 AWS 服務的自動路由表更新
- ✅ 體驗 Infrastructure as Code 的精確控制

**重要架構理解**：
- 🔄 **Gateway vs Interface Endpoints**：成本 vs 功能權衡
- 🧠 **CDK 智慧推論**：`SubnetType` vs `SubnetGroupName`
- 🔧 **AWS 服務代管**：自動路由表更新機制
- 💰 **成本優化策略**：免費 Gateway 優於付費 Interface

**準備進入步驟 6：Security Groups**！您即將學習 AWS 網路安全的核心組件！

## 🎉 恭喜！階段二：網路基礎 完全完成！

您已經成功建立了完整的 AWS 網路架構：
- ✅ **VPC 架構**：三層網路設計 (Public/App/Database)
- ✅ **路由控制**：深入理解路由表運作機制
- ✅ **成本優化**：S3 Gateway Endpoint 免費私有存取
- ✅ **安全防護**：Security Groups 最小權限設計
- ✅ **智慧整合**：Security Group 引用動態控制

**重要架構技能**：
- 🏗️ **網路分層**：Public/Private 子網路隔離
- 🔒 **安全設計**：多層次防護機制
- 💰 **成本意識**：Gateway vs Interface Endpoints 權衡
- 🎯 **最佳實務**：AWS Well-Architected 網路原則

**CDK 進階技能**：
- 🧠 **Infrastructure as Code**：程式化的基礎設施管理
- 🔄 **智慧推論**：CDK 自動決策與手動控制的平衡
- 🎯 **精確控制**：SubnetGroupName vs SubnetType 的選擇
- 🚀 **版本控制**：基礎設施變更的追蹤和管理

**準備進入階段三：運算服務**！您即將學習 ECS 容器編排的核心技能！

## 🎉 恭喜！階段一：CDK 基礎 完全完成！

您已經成功：
- ✅ 設定開發環境
- ✅ 配置 AWS CLI  
- ✅ 建立學習計劃
- ✅ 學會基本的 Git 操作
- ✅ 理解 CDK 專案結構
- ✅ 掌握 CDK 基本指令
- ✅ 完成 CDK Bootstrap
- ✅ 部署第一個 VPC
- ✅ 應用成本優化思維
- ✅ 驗證 AWS 資源
- ✅ 深度理解 Infrastructure as Code 概念
- ✅ 體驗 CDK 的智慧決策引擎

**準備進入階段二：網路基礎**: 現在可以深入學習路由表的配置和運作原理！

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