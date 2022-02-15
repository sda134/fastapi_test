
#include <gtk/gtk.h>

GtkWidget *label1;
GtkWidget *button1;
GtkWidget *mainwindow;

GtkBuilder *builder;

int main(int argc, char *argv[])
{	 
	// initialise
	gtk_init(&argc,&argv);
	
	// get builder
    builder = gtk_builder_new_from_file("glade_test.glade");
    mainwindow = GTK_WIDGET(gtk_builder_get_object(builder, "main_window"));
    g_signal_connect(mainwindow, "destroy", G_CALLBACK(gtk_main_quit), NULL);

    gtk_builder_connect_signals(builder, NULL);

	// get object
    label1 = GTK_WIDGET(gtk_builder_get_object(builder, "label1"));
	 
	// show and run
	gtk_widget_show(mainwindow);
	gtk_main();
    
    return 0;
}

void button1_clicked_cb(GtkButton *button, gpointer user_data)
{
	gtk_label_set_text(GTK_LABEL(label1), (const gchar*)"Hello World!");
}

