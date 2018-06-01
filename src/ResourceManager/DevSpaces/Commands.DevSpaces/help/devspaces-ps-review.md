# Azure dev spaces Service

## Planned PowerShell Release Milestone
- 2018-06-01

## Service Release Details
- Public Preview on 2018-05-07

## Contact Information
- Main developer contacts
   - [Menghua Xiao](menxiao@microsoft.com), [ArieShout](https://github.com/ArieShout)
   - [Chenyang Liu](chenyl@microsoft.com), [zackliu](https://github.com/zackliu)
   - [Ken Chen](kenchen@microsoft.com), [chenkennt](https://github.com/chenkennt)
- PM contact
   - [Zhidi Shang](zhshang@microsoft.com), [sffamily](https://github.com/sffamily)
- Other people who should attend a design review
   - vscsignalr@microsoft.com

## High Level Scenarios

Azure Dev Spaces helps you develop with speed on Kubernetes. With Azure Dev Spaces, you also add full development capabilities such as debugging to your Azure Kubernetes containers, and you can iteratively develop containers in the cloud using familiar tools like VS Code, Visual Studio, or the command line. Azure Dev Spaces is especially relevant in team development where isolation of individual code branches in their own spaces is a critical part of the development lifecycle.

The typical end-to-end usage for the azure dev spaces service would be:

1. The user creates a Azure Dev Spaces controller through ARM using 'New-AzureRmDevSpacesController' cmdlet.
2. The user creates a web application using VSCode / Visual Studio.
3. The user selects Azure Dev Spaces in the list of emulator and debug the web application in AKS cluster.
4. The user deletes the Dev Spaces Controller using 'Remove-AzureRmDevSpacesController' 

# Syntax changes

## New Cmdlet - 'Get-AzureRmDevSpacesController' 

The `Get-AzureRmDevSpacesController` cmdlet gets a specific Dev Spaces controller or all the dev space controller in a resource group or a subscription.

```powershell
PS C:\> Get-AzureRmDevSpacesController [-ResourceGroupName <String>] [-Name <String>] [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]

Name        Resource Group  Location  Provisioning State
----------  --------------  --------  ------------------
devspaces   devspacesrg     eastus    Succeeded
```

## New Cmdlet - 'New-AzureRmDevSpacesController' 

The `New-AzureRmDevSpacesController` cmdlet creates a Dev Spaces controller.

```powershell
PS C:\> New-AzureRmDevSpacesController -ResourceGroupName <String> -Name <String> -AKSResourceGroupName <String> -AKSClusterName <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### New Cmdlet - `Remove-AzureRmDevSpacesController`

The `Remove-AzureRmDevSpacesController` cmdlet removes a Dev Spaces controller.

```powershell
PS C:\> Remove-AzureRmDevSpacesController -ResourceGroupName <String> -Name <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

