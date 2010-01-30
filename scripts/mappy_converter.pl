@files = glob("../ggj2010/Content/*.CSV");

foreach $file (@files)
{
	open FILE, "<$file";
	@lines = <FILE>;
	close FILE;
	$count = $lines[0] =~ tr/,//;
	$num_lines = 0;
	$processed = 0;
	$cat = "";
	$text = "";
	@new_lines = ();
	foreach $line (@lines)
	{
		if(!($line =~ /\d,/))
		{
			next;
		}
		$processed++;
		if($line =~ /, $/)
		{
			chomp $line;
			$cat .= $line;
		}
		else
		{
			chomp $line;
			$cat .= $line;
			$cat =~ s/,//g;
			$text .= $cat . "\n";
			$cat = "";
			$num_lines = $processed if($num_lines eq 0);
		}
	}
	$new_file = $file;
	$new_file =~ s/CSV/txt/g;
	open FILE, ">$new_file";
	$width = $num_lines * $count;
	$height = $processed / $num_lines;
	print FILE "$width $height\n";
	print FILE "$text";
}

