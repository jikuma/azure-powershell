<#
.SYNOPSIS
Test Kubernetes stuff
#>
function Test-AzureRmDevSpacesController
{
    # Setup
    $resourceGroupName = Get-RandomResourceGroupName
    $kubeClusterName = Get-RandomClusterName
	$devSpacesName = Get-DevSpacesControllerName
	$location = "West US"

    try
    {
		New-AzureRmDevSpacesController -ResourceGroupName $resourceGroupName -Name $devSpacesName -TargetClusterName $kubeClusterName -TargetResourceGroupName $resourceGroupName
		
		$devSpaceController = Get-AzureRmDevSpacesController -ResourceGroupName $resourceGroupName -Name $devSpacesName

		Assert-NotNull $cluster.Fqdn
		Assert-NotNull $cluster.DnsPrefix
		Assert-AreEqual 1 $cluster.AgentPoolProfiles.Length
		Assert-AreEqual "1.8.1" $cluster.KubernetesVersion
		Assert-AreEqual 3 $cluster.AgentPoolProfiles[0].Count;
		$cluster = $cluster | Set-AzureRmAks -NodeCount 2
		Assert-AreEqual 2 $cluster.AgentPoolProfiles[0].Count;
		$cluster | Import-AzureRmAksCredential -Force
		$cluster | Remove-AzureRmAks -Force

    }
    finally
    {
        Remove-AzureRmResourceGroup -Name $resourceGroupName -Force
    }
}