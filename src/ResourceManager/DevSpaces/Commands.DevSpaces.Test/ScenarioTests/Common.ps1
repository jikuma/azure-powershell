# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

<#
.SYNOPSIS
Gets cluster name
#>
function Get-RandomClusterName
{
    Write-Host "called RandomClusterName";
    return 'kube' + (getAssetName)
}

<#
.SYNOPSIS
Gets DevSpaces controller name
#>
function Get-DevSpacesControllerName
{
	Write-Host "called DevSpacesControllerName";
    return 'devspaces' + (getAssetName)
}

<#
.SYNOPSIS
Gets resource group name
#>
function Get-RandomResourceGroupName
{
	Write-Host "called RandomResourceGroupName";
    return 'rg' + (getAssetName)
}

function Assert-Error
{
	param([ScriptBlock] $script, [string] $message)

	$originalErrorCount = $error.Count
	$originalErrorActionPreference = $ErrorActionPreference
	$ErrorActionPreference = "SilentlyContinue"
	try
	{
		&$script
	}
	finally
	{
		$ErrorActionPreference = $originalErrorActionPreference
	}

	$result = $Error[0] -like "*$($message)*"

	If(!$result)
	{
		 Write-Output "expected error $($message), actual error $($Error[0])"
	}

	Assert-True {$result}

	$Error.Clear()
}