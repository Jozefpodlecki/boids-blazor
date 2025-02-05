$filePath = $args[0]
$newBaseHref = $args[1]

$reader = [System.IO.StreamReader]::new($filePath)
$content = $reader.ReadToEnd()
$reader.Close()

$content = $content -replace "%BASE_HREF%", $newBaseHref

$writer = [System.IO.StreamWriter]::new($filePath, $false)
$writer.Write($content)
$writer.Close()