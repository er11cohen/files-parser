interface BookTitles {
	titles: Array<BookTitle>;
}

interface BookTitle extends Title {
	bookSubTitles: Array<Title>;
}

interface Title{
	id: number;
	title: string;
}